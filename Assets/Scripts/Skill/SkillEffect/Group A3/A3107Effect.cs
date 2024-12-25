using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3107Effect")]
public class A3107Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("BeforePlayerAttackEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("BeforePlayerAttackEvent", EventSkill);
    }

    public override void Execute()
    {

    }

    public override void ImmediateTrigger()
    {

    }

    public void EventSkill()
    {
        // 用于存储字符及其出现次数的字典
        Dictionary<string, int> cardCount = new Dictionary<string, int>();

        foreach (var card in AllyPoint.Instance.holder.cards)
        {
            if (cardCount.ContainsKey(card.name))
            {
                cardCount[card.name]++;
            }
            else
            {
                cardCount[card.name] = 1;
            }
            
        }

        int count = 0;
        foreach (var kvp in cardCount)
        {
            if (kvp.Value >= 2)
            {
                count++;
            }
        }
        Debug.Log("重复卡牌数量：" + count);
        GamePointBoard.Instance.attack+=6*count;
        GamePointBoard.Instance.currentHealth+=8*count;

    }
    
    public override void Interrupt()
    {
            
    }
}
