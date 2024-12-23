using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/A102Effect")]
public class A102Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe<List<Card>>("PlayerTurnStartEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Subscribe<List<Card>>("PlayerTurnStartEvent", EventSkill);
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }

    public void EventSkill(List<Card> cards)
    {
        //检查列表中前三项 获取三张牌中最大点数和最小点数
        if (cards.Count < 3)
        {
            Debug.Log("A102Effect:牌数量不足3张");
        }
        else
        {
            //获取cards中前三张牌里最大点数和最小点数
            int max = cards[0].points;
            int min = cards[0].points;
            for (int i = 1; i < 3; i++)
            {
                if (cards[i].points > max)
                {
                    max = cards[i].points;
                }
                if (cards[i].points < min)
                {
                    min = cards[i].points;
                }
            }
            Debug.Log("A102Effect:最大点数" + max + "最小点数" + min);
        }
    }
}
