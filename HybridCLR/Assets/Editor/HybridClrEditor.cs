using UnityEditor;
using UnityEngine;
public static class HybridClrEditor
{
    [MenuItem ("HybridCLR/OpenPersistentDataPath", false, 1)]
    public static void OpenPersistentDataPath ()
    {
        Application.OpenURL (Application.persistentDataPath);
    }
}