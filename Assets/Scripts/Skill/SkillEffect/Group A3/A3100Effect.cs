using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3100Effect")]
public class A3100Effect : BaseEffect
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
        GamePointBoard.Instance.canFindTreasure = true;
    }
    public void EventSkill()
    {
        foreach (var card in AllyPoint.Instance.holder.cards)
        {
            if (card.isTreasure == true)
            {
                GamePointBoard.Instance.attackMultiple *= 2;
            }
        }
    }

    public override void Interrupt()
    {
        
    }
}
