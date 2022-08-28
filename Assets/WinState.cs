using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    private WinBox DavidWin;
    private WinBox SteveWin;

    private Animator DavidAnim;
    private Animator SteveAnim;

    public UnityEvent winEvent;

    private bool isSucking = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        var davidwin = GameObject.Find("DavidWin");
        if(davidwin)
            DavidWin = davidwin.GetComponent<WinBox>();

        var stevewin = GameObject.Find("SteveWin");
        if (stevewin)
            SteveWin = stevewin.GetComponent<WinBox>();

        var david = GameObject.Find("David");
        if (david)
            DavidAnim = david.GetComponentInChildren<Animator>();
        var steve = GameObject.Find("Steve Variant");
        if (steve)
            SteveAnim = steve.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(!isSucking && (DavidWin == null || DavidWin.IsInBox()) && (SteveWin == null || SteveWin.IsInBox()))
        {
            isSucking = true;
            StartCoroutine(waiter());
            Debug.Log("Actually Win!!!!");
        }
    }

    IEnumerator waiter()
    {
        if (DavidAnim)
        {
            DavidAnim.SetTrigger("Shrink");
            DavidAnim.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            DavidAnim.transform.parent.position = DavidWin.transform.position;
        }
        if (SteveAnim)
        {
            SteveAnim.SetTrigger("Shrink");
            SteveAnim.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            SteveAnim.transform.parent.position = SteveWin.transform.position;
        }

        AudioManager.instance.Play("Correct");

        yield return new WaitForSeconds(1.3f);

        winEvent?.Invoke();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //isSucking = false;
    }
}
