using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PerformanceMgr : MonoBehaviour
{
    public Button buttonHotFix;
    public Text output;
    
    Assembly ass;
    private MethodInfo methodTest;
    void Start()
    {
        Application.logMessageReceived+=HandleLog;
        buttonHotFix.onClick.AddListener(HotFixCall);
        InitEnv();
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output.text = output.text += "\n"+logString;
    }

    private void HotFixCall()
    {
        methodTest.Invoke(null, null);
    }

    void InitEnv()
    {
#if UNITY_EDITOR
        string dllPath = Path.Combine(Application.dataPath,"../HybridCLRData/HotUpdateDlls/Android/HotFix.dll");
#else
        string dllPath = Path.Combine(Application.persistentDataPath,"HotFix.dll");
#endif
        if(File.Exists(dllPath))
        {
            byte[] assemblyData = File.ReadAllBytes(dllPath);
            string str=Convert.ToBase64String(assemblyData);
            ass = Assembly.Load(assemblyData);
            Type entryType = ass.GetType("HotFixClass");
            if(null == entryType)
            {
                Debug.LogError("entryType is null");
                return;
            }
            MethodInfo method = entryType.GetMethod("HotFixFunc");
            if(null == method)
            {
                Debug.LogError("method is null");
                return;
            }

            Func<int,int,int[], bool> checkFunc = (Func<int,int,int[], bool>)Delegate.CreateDelegate(typeof(Func<int,int,int[], bool>), method);
            Native.InitFunc(checkFunc);

            Type entryTypeHot = ass.GetType("PerformanceHot");
            if(null == entryTypeHot)
            {
                Debug.LogError("entryTypeHot is null");
                return;
            }
            methodTest = entryTypeHot.GetMethod("Call");
            if(null == methodTest)
            {
                Debug.LogError("methodTest is null");
                return;
            }
        }
    }

}
