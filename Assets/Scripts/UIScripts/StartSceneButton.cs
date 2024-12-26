using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartSceneButton : MonoBehaviour
{
    public Button playButton; // 在编辑器中拖放按钮
    public VideoClip beginning; // 在编辑器中拖放VideoClip
    public VideoPlayer video;
    public string nextSceneName; // 视频播放完毕后要切换的场景名称

    void Start()
    {
        // 添加按钮点击事件监听器
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        Debug.Log("开始播放视频");
        // 启用并播放视频
        video.clip = beginning;
        video.Play();

        // 监听视频播放完毕事件
        video.loopPointReached += OnVideoEnded;
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        // 切换到指定场景
        Debug.Log("视频播放完毕，切换场景");
        SceneManager.LoadScene(nextSceneName);
    }
}

