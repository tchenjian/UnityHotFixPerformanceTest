using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

public static class PerformanceHot
{
    private static void Test0 ()
    {
        var go = new GameObject ("t");
        var transform = go.transform;

        var cnt = 1024 * 1000;
        for (var i = 0; i < cnt; i++)
        {
            transform.position = transform.position;
        }

        Object.Destroy (go);
    }

    private static void Test1 ()
    {
        var go = new GameObject ("t");
        var transform = go.transform;

        var cnt = 1024 * 100;
        for (var i = 0; i < cnt; i++)
        {
            transform.Rotate (Vector3.up, 1);
        }

        Object.Destroy (go);
    }

    private static void Test2 ()
    {
        var cnt = 1024 * 1000;
        for (var i = 0; i < cnt; i++)
        {
            var v = new Vector3 (i, i, i);
            var x = v.x;
            var y = v.y;
            var z = v.z;
            var r = x + y * z;
        }
    }

    private static void Test3 ()
    {
        var cnt = 1024 * 10;
        for (var i = 0; i < cnt; i++)
        {
            var go = new GameObject ("t");
            Object.Destroy (go);
        }
    }

    private static void Test4 ()
    {
        var cnt = 1024 * 10;
        for (var i = 0; i < cnt; i++)
        {
            var go = new GameObject ();
            go.AddComponent<SkinnedMeshRenderer> ();
            var c = go.GetComponent<SkinnedMeshRenderer> ();
            c.receiveShadows = false;
            Object.Destroy (go);
        }
    }

    private static void Test5 ()
    {
        var cnt = 1024 * 1000;
        for (var i = 0; i < cnt; i++)
        {
            var p = Input.mousePosition;
        }
    }

    private static void Test6 ()
    {
        var cnt = 1024 * 1000;
        for (var i = 0; i < cnt; i++)
        {
            var v = new Vector3 (i, i, i);
            Vector3.Normalize (v);
        }
    }

    private static void Test7 ()
    {
        var cnt = 1024 * 100;
        for (var i = 0; i < cnt; i++)
        {
            var q1 = Quaternion.Euler (i, i, i);
            var q2 = Quaternion.Euler (i * 2, i * 2, i * 2);
            Quaternion.Slerp (Quaternion.identity, q1, 0.5f);
        }
    }

    private static void Test8 ()
    {
        long total = 0;
        var cnt = 1024 * 10000;
        for (var i = 0; i < cnt; i++)
        {
            total = total + i - (i / 2) * (i + 3) / (i + 5);
        }
    }

    private static void Test9 ()
    {
        var cnt = 1024 * 1000;
        for (var i = 0; i < cnt; i++)
        {
            var a = new Vector3 (1, 2, 3);
            var b = new Vector3 (4, 5, 6);
            var c = a + b;
        }
    }

        private static int[] intts;
        private static void Test10()
        {
            intts = new int[1024];
            for (var i = 1; i < 1025; i++)
            {
                intts[i-1] = i;
            }
        }

        private static void Test12()
        {
            int total = 0;
            var cnt = 1024 * 1000;
            for (var i = 0; i < cnt; i++)
            {
                for (var j = 0; j < 1024; j++)
                {
                    total = total + intts[j];
                }
            }
        }

    private static int[] ints;
    private static void Test11 ()
    {
        Native.TestFunc1 (ints, 5258);
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
        string[] dt = text.text.Split ('-');
        ints = new int[dt.Length];
        for (var i = 0; i < dt.Length; i++)
        {
            ints[i] = int.Parse (dt[i]);
        }
    }
}