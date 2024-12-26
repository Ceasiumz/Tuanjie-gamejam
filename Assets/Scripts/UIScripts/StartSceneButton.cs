using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartSceneButton : MonoBehaviour
{
    public Button playButton; // �ڱ༭�����ϷŰ�ť
    public VideoClip beginning; // �ڱ༭�����Ϸ�VideoClip
    public VideoPlayer video;
    public string nextSceneName; // ��Ƶ������Ϻ�Ҫ�л��ĳ�������

    void Start()
    {
        // ��Ӱ�ť����¼�������
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        Debug.Log("��ʼ������Ƶ");
        // ���ò�������Ƶ
        video.clip = beginning;
        video.Play();

        // ������Ƶ��������¼�
        video.loopPointReached += OnVideoEnded;
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        // �л���ָ������
        Debug.Log("��Ƶ������ϣ��л�����");
        SceneManager.LoadScene(nextSceneName);
    }
}

