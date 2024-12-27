using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A111Effect")]
public class A111Effect : BaseEffect
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
        foreach (var card in AllyPoint.Instance.holder.cards)
        {
            if (card.name=="7")
            {
                GamePointBoard.Instance.attack += 7;
            }
        }
        
    }

    public override void Interrupt()
    {
        
    }
}
