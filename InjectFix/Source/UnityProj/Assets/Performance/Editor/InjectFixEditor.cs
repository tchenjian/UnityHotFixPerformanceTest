using UnityEditor;
using UnityEngine;

public static class InjectFixEditor
{
    [MenuItem ("InjectFix/OpenPersistentDataPath", false, 1)]
    public static void OpenPersistentDataPath ()
    {
        Application.OpenURL (Application.persistentDataPath);
    }
}