using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    public Button clickbutton; // 在编辑器中拖放按钮
    public Button clickbutton2;
    public Button clickbutton3;
    public AudioSource audioSource; // 在编辑器中拖放AudioSource组件

    void Start()
    {
        // 添加按钮点击事件监听器
        clickbutton.onClick.AddListener(OnButtonClicked);
        clickbutton2.onClick.AddListener(OnButtonClicked);
        clickbutton3.onClick.AddListener(OnButtonClicked);

    }

    void OnButtonClicked()
    {
        // 播放音效
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
