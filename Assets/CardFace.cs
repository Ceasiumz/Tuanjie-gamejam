using UnityEngine;
using UnityEngine.UI;

public class CardFace : MonoBehaviour
{
    public GameObject spriteObject;
    public Image sprite;
    public Sprite back;
    public Sprite front;
    private void Start()
    {
        if(spriteObject != null){
            sprite = spriteObject.GetComponent<Image>();
        }
    }

    public void SetCardFace(string cardName, CardSuit suit)
    {
        // 获取parentCard的点数和花色信息，这里假设parentCard有对应的属性来表示
        //string cardPoints = points.ToString();
        string cardSuit = suit.ToString();

        // 根据点数和花色构建对应的图片资源名称
        string resourceName = cardSuit + "/" + cardName;

        // 从Resources文件夹加载对应的图片资源
        Sprite cardSprite = Resources.Load<Sprite>(resourceName);
        front = cardSprite;

        if (cardSprite!= null)
        {
            // 将CardFace的Image组件的SourceImage属性替换为加载到的图片
            //sprite.sprite = cardSprite;
        }
        else
        {
            Debug.LogError("无法找到对应的卡牌图片: " + resourceName);
        }
    }
}
