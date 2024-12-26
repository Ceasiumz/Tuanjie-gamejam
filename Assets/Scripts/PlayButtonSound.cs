using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    public Button Selectbutton; // 在编辑器中拖放按钮
    public AudioSource audioSource; // 在编辑器中拖放AudioSource组件

    void Start()
    {
        // 添加按钮点击事件监听器
        Selectbutton.onClick.AddListener(OnButtonClicked);
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
