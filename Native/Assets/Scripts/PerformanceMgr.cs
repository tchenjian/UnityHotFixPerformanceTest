using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PerformanceMgr : MonoBehaviour
{
    public Button buttonNative;
    public Text output;
    
    void Start()
    {
        Application.logMessageReceived+=HandleLog;
        buttonNative.onClick.AddListener(NativeCall);
        InitEnv();
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output.text = output.text += "\n"+logString;
    }

    private void NativeCall()
    {
        Performance.Call();
    }

    void InitEnv()
    {
    }

}
