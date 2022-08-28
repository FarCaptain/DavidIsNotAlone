using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtilities : MonoBehaviour
{
    private Scene currentScene;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
