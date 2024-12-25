using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonTween : MonoBehaviour
{
    private Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        // 为按钮添加点击事件监听器
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // 对按钮进行缩放动画
        transform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 0.2f)
           .SetEase(Ease.OutBack)
           .OnComplete(() =>
            {
                // 动画完成后恢复原始大小
                transform.DOScale(new Vector3(2f, 2f, 2f), 0.2f).SetEase(Ease.InBack);
            });
    }
}