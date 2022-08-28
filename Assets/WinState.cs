using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinState : MonoBehaviour
{
    private WinBox DavidWin;
    private WinBox SteveWin;

    public UnityEvent winEvent;

    void Start()
    {
        var david = GameObject.Find("DavidWin");
        if(david)
            DavidWin = david.GetComponent<WinBox>();

        var steve = GameObject.Find("SteveWin");
        if (steve)
            SteveWin = steve.GetComponent<WinBox>();
    }

    private void Update()
    {
        if((DavidWin == null || DavidWin.IsInBox()) && (SteveWin == null || SteveWin.IsInBox()))
        {
            Debug.Log("Actually Win!!!!");
            winEvent?.Invoke();
        }
    }
}
