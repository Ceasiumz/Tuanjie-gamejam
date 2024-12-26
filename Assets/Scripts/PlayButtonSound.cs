using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    public Button clickbutton; // �ڱ༭�����ϷŰ�ť
    public Button clickbutton2;
    public Button clickbutton3;
    public AudioSource audioSource; // �ڱ༭�����Ϸ�AudioSource���

    void Start()
    {
        // ��Ӱ�ť����¼�������
        clickbutton.onClick.AddListener(OnButtonClicked);
        clickbutton2.onClick.AddListener(OnButtonClicked);
        clickbutton3.onClick.AddListener(OnButtonClicked);

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
