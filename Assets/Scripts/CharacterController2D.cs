using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private Animator m_Animator;

	const float k_GroundedRadius = .4f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private bool mounting = false;
	private bool collided = false;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();

				// if we have two players with different color stacking each other
				// make them move together
				// disable the rb constraint
				// parenting
				if(colliders[i].gameObject.tag == "Player")
                {
					var otherPlayer = colliders[i].gameObject;
					string otherLayerName = LayerMask.LayerToName(otherPlayer.layer);
					string myLayerName = LayerMask.LayerToName(gameObject.layer);

					if( (otherLayerName.Contains("Red") && myLayerName.Contains("Blue")) ||
						(otherLayerName.Contains("Blue") && myLayerName.Contains("Red")) )
                    {
						var rgd = gameObject.GetComponent<Rigidbody2D>();
						bool enableCon = gameObject.GetComponent<PlayerMovement>().enableControl;
						if (!enableCon && !collided)
						{
							if (mounting)
								return;

							// cancle the constraint for avoiding pushing
                            rgd.constraints = RigidbodyConstraints2D.FreezeRotation;
                            rgd.bodyType = RigidbodyType2D.Kinematic;
                            transform.parent = otherPlayer.transform;
							mounting = true;
						}
                        else
                        {
							// a. pushed down  b.enableControl
							if(!enableCon)
								rgd.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
							rgd.bodyType = RigidbodyType2D.Dynamic;
                            transform.parent = otherPlayer.transform.parent;
							mounting = false;
                        }
					}
				}
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

            move *= m_CrouchSpeed;

            // Enable the collider when not crouching
            if (m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = true;

            if (m_wasCrouching)
            {
                m_wasCrouching = false;
                OnCrouchEvent.Invoke(false);
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			// play Anmation here
			m_Animator.SetTrigger("Jump");
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
			collided = true;
        }
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag != "Player")
		{
			collided = false;
		}
	}
}
