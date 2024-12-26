using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    public Button Selectbutton; // �ڱ༭�����ϷŰ�ť
    public AudioSource audioSource; // �ڱ༭�����Ϸ�AudioSource���

    void Start()
    {
        // ��Ӱ�ť����¼�������
        Selectbutton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        // ������Ч
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
