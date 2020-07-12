using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Credits : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    public void OpenLinkFelipe()
    {
#if UNITY_EDITOR || !UNITY_WEBGL
        Application.OpenURL("https://fiwon123.itch.io/");
#else
        openWindow("https://fiwon123.itch.io/");
#endif
    }

    public void OpenLinkEuler()
    {
#if UNITY_EDITOR || !UNITY_WEBGL
        Application.OpenURL("https://euler-moises.itch.io/");
#else
        openWindow("https://euler-moises.itch.io/");
#endif
    }
}
