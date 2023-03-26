using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using IFix.Core;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PerformanceMgr : MonoBehaviour
{
    public Button buttonHotFix;
    public Text output;
    void Start()
    {
        Application.logMessageReceived+=HandleLog;
        buttonHotFix.onClick.AddListener(HotFixCall);
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output.text = output.text += "\n"+logString;
    }

    private void HotFixCall()
    {
        InitEnv();
        PerformanceHot.Call();
    }

    void InitEnv()
    {
        VirtualMachine.Info = (s) => UnityEngine.Debug.Log(s);
        //try to load patch for Assembly-CSharp.dll
        string dllPath = Path.Combine(Application.persistentDataPath,"Assembly-CSharp.patch.bytes");
        if(File.Exists(dllPath))
        {
            UnityEngine.Debug.Log("loading Assembly-CSharp.patch ...");
            var sw = Stopwatch.StartNew();
            PatchManager.Load(new MemoryStream(File.ReadAllBytes(dllPath)));
            UnityEngine.Debug.Log("patch Assembly-CSharp.patch, using " + sw.ElapsedMilliseconds + " ms");
        }
        //try to load patch for Assembly-CSharp-firstpass.dll
        dllPath = Path.Combine(Application.persistentDataPath,"Assembly-CSharp-firstpass.patch.bytes");
        if(File.Exists(dllPath))
        {
            UnityEngine.Debug.Log("loading Assembly-CSharp-firstpass ...");
            var sw = Stopwatch.StartNew();
            PatchManager.Load(new MemoryStream(File.ReadAllBytes(dllPath)));
            UnityEngine.Debug.Log("patch Assembly-CSharp-firstpass, using " + sw.ElapsedMilliseconds + " ms");
        }
    }
}
