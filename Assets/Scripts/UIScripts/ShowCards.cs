using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCards : MonoBehaviour
{
    public Text logText; // 拖放UI Text组件到此处
    private Queue<string> logQueue = new Queue<string>();
    public int maxLogCount = 10; // 显示的最大日志条数

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // 过滤特定的Debug.Log消息
        if (logString.StartsWith("运筹帷幄"))
        {
            logQueue.Enqueue(logString);
            if (logQueue.Count > maxLogCount)
            {
                logQueue.Dequeue();
            }

            logText.text = string.Join("\n", logQueue.ToArray());
        }
    }
}
