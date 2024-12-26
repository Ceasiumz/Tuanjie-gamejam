using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3100Effect")]
public class A3100Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        Debug.Log("技能事件订阅 A3100");
        DynamicEventBus.Subscribe("BeforePlayerAttackEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        GamePointBoard.Instance.canFindTreasure = false;
        DynamicEventBus.Unsubscribe("BeforePlayerAttackEvent", EventSkill);
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        GamePointBoard.Instance.canFindTreasure = true;
    }
    public void EventSkill()
    {
        foreach (var card in AllyPoint.Instance.holder.cards)
        {
            if (card.isTreasure == true&&GamePointBoard.Instance.canFindTreasure == true)
            {
                Debug.Log("触发技能A3100 伤害倍率翻倍 宝牌"+card.name);
                GamePointBoard.Instance.attackMultiple *= 2;
            }
        }
    }

    public override void Interrupt()
    {
        
    }
}
