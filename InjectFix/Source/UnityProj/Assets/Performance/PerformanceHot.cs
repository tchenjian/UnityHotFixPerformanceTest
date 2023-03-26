using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public static class PerformanceHot
{
    private static void Test0 ()
    {
        Debug.LogError("Test0");
    }

    private static void Test1 ()
    {
        Debug.LogError("Test1");
    }

    private static void Test2 ()
    {
        Debug.LogError("Test2");
    }

    private static void Test3 ()
    {
        Debug.LogError("Test3");
    }

    private static void Test4 ()
    {
        Debug.LogError("Test4");
    }

    private static void Test5 ()
    {
        Debug.LogError("Test5");
    }

    private static void Test6 ()
    {
        Debug.LogError("Test6");
    }

    private static void Test7 ()
    {
        Debug.LogError("Test7");
    }

    private static void Test8 ()
    {
        Debug.LogError("Test8");
    }

    private static void Test9 ()
    {
        Debug.LogError("Test9");
    }

    private static int[] intts;
    private static void Test10()
    {
        Debug.LogError("Test10");
    }

    private static int[] ints;
    private static void Test11 ()
    {
        Debug.LogError("Test11");
    }

    private static void Test12()
    {
        Debug.LogError("Test12");
    }

    private static readonly Dictionary<string, Action> STestAction =
        new Dictionary<string, Action>
        {
            ["t0"] = Test0,
            ["t1"] = Test1,
            ["t2"] = Test2,
            ["t3"] = Test3,
            ["t4"] = Test4,
            ["t5"] = Test5,
            ["t6"] = Test6,
            ["t7"] = Test7,
            ["t8"] = Test8,
            ["t9"] = Test9,
            ["t10"] = Test10,
            ["t11"] = Test11,
            ["t12"] = Test12,
        };

    private static float CallFunc (string name)
    {
        if (!STestAction.TryGetValue (name, out var act) || act == null)
        {
            // SimpleLog.Log($"[Performance::CallFunc]can't find test action :{name}");
            return -1;
        }

        var sw = Stopwatch.StartNew ();
        act ();
        sw.Stop ();
        return (float)((double)sw.ElapsedTicks / 10000);
    }

    public static void Call ()
    {
        string tag = "[PerformanceHotFix]";
        InitTest11 ();
        var sb = new StringBuilder ();
        foreach (var kv in STestAction)
        {
            sb.Append ($"{kv.Key}:{CallFunc(kv.Key):F2}ms ");
        }

        UnityEngine.Debug.LogError($"{tag} {sb}");
    }

    private static void InitTest11 ()
    {
        var text = Resources.Load<TextAsset> ("5258");
        // UnityEngine.Debug.LogError (text.text);
        string[] dt = text.text.Split ('-');
        ints = new int[dt.Length];
        for (var i = 0; i < dt.Length; i++)
        {
            ints[i] = int.Parse (dt[i]);
        }
    }
}