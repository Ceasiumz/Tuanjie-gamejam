using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemysSkill : MonoBehaviour
{
    public GameObject E0;
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E6;
    public GameObject E7;
    public GameObject showPannel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseEnter()
    {
        showPannel.SetActive(true);
        if(E0.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "存在于电脑中的神秘生物，没有人知道“他”的来历。这家伙虽然外表看似可怕，" +
                "但是却只会遵循本能攻击，是个十分好战胜的对手";
        }
        else if (E1.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "不明来历的女子"  + "\n" +
                "技能：憎恶之雷" + "\n" + "效果：当玩家每次抽到黑桃2-9时，必须再抽一张牌";
        }
        else if(E2.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "戴着面具的神秘男子，看不清年龄与长相"  + "\n" +
                "技能：扰攘宁谧" + "\n" + "效果：玩家每次抽牌时，就会获取一张点数为2的草花牌"; 
        }
        else if(E3.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "一无所有,四处游荡的亡魂,会攻击一切生物" + "\n" +
                "技能：往日不在" + "\n" + "效果：标记玩家随机一张牌，在玩家下个回合的抽牌之前，弃置那张被标记的牌";
        }
        else if(E6.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "来路不明..." + "\n" +
                "技能：不可饶恕" + "\n" + "效果：受到伤害后，玩家下一个回合每张手牌的点数+1，这个效果最多叠加两次";
        }
        else if(E7.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "此地最古老的存在，很多人猜测她就是轮回的真凶，但真相没有人知道" + "\n" +
                "技能：【数据删除】" + "\n" + "效果：只要玩家手牌中存在至少一张不会使自己爆牌的牌，则从玩家手中获得那些牌中点数最大的牌";
        }
    }
    public void OnMouseExit()
    {
        showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "";
        showPannel.SetActive(false);
    }
}
