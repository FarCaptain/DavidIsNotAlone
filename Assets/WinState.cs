using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    private WinBox DavidWin;
    private WinBox SteveWin;

    void Start()
    {
        var david = GameObject.Find("DavidWin");
        if(david)
        {
            
        }
        var steve = GameObject.Find("SteveWin");
        //SteveWin = GameObject.Find("SteveWin").GetComponent<WinBox>();
    }

    private void Update()
    {
        
    }
}
