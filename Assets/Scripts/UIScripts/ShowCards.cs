using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCards : MonoBehaviour
{
    public Text logText; // �Ϸ�UI Text������˴�
    private Queue<string> logQueue = new Queue<string>();
    public int maxLogCount = 10; // ��ʾ�������־����

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
        // �����ض���Debug.Log��Ϣ
        if (logString.StartsWith("�˳���"))
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
