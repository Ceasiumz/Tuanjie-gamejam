using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A102Effect")]
public class A102Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        TurnManager.Instance.PlayerTurn_Start.AddListener(EventSkill);
        Debug.Log("技能事件订阅");
    }

    public override void unsubscribeEvent()
    {
        // TurnManager.Instance.PlayerTurn_Start.RemoveListener(EventSkill);
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }

    public void EventSkill()
    {
        int max=0;
        int min=15;
        
            if(CardDack.Instance.cardsDeck.Count>3)
            {
                //查找下三张牌中最大的点数和最小的点数
                int i = 0;
                while (i<3)
                {
                    int temPoint;
                    switch (CardDack.Instance.cardsDeck[i].point)
                    {
                        case "A":
                            temPoint = 1;
                            break;
                        case "J":
                            temPoint = 10;
                            break;
                        case "Q":
                            temPoint = 10;
                            break;
                        case "K":
                            temPoint = 10;
                            break;
                        default:
                            if (int.TryParse(CardDack.Instance.cardsDeck[i].point, out int parsedPoint))
                            {
                                temPoint = parsedPoint;
                            }
                            else
                            {
                                Debug.LogError($"无法解析牌面点数 {CardDack.Instance.cardsDeck[i].point}，设置默认点数为0" + "技能A102错误");
                                temPoint = 0;
                            }
                            break;
                    }
                    Debug.Log("A102本次点数"+temPoint);
                    if (temPoint > max)
                    {
                        max=temPoint;
                    }
                    if(temPoint<min)
                    {
                        min=temPoint;
                    }
                    i++;
                }
                Debug.Log("A102Effect:最大点数" + max + "最小点数" + min);
            }

    }
    

    public override void Interrupt()
    {
        
    }
}
