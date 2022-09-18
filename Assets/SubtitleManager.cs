using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;
    public Text subtitle;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Text("Hi There!");
    }


    public void Text(string str)
    {
        subtitle.text = str;
        subtitle.gameObject.SetActive(true);

        StartCoroutine(textVanish());
    }

    IEnumerator textVanish()
    {
        yield return new WaitForSeconds(5.0f);
        subtitle.gameObject.SetActive(false);
    }
}
