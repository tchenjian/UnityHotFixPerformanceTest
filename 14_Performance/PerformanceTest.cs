using UnityEngine;
using XLua;
using UnityEngine.UI;

namespace XLuaTest
{
    public class PerformanceTest : MonoBehaviour
    {
        LuaEnv luaenv = null;
        public Text output;
        public Button lua;
        // Use this for initialization
        void Start()
        {
            Application.logMessageReceived+=HandleLog;
            lua.onClick.AddListener(StartLuaTest);
        }

        private void StartLuaTest()
        {
            luaenv = new LuaEnv();
            luaenv.DoString("require 'Performance'");
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            output.text = output.text += "\n"+logString;
        }

        void OnDestroy()
        {
            luaenv.Dispose();
        }
    }
}
