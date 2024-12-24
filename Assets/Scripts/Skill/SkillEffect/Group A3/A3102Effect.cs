using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3102Effect")]
public class A3102Effect : BaseEffect
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
        if (AllyPoint.Instance.holder.cards.Count==2)
        {
            GamePointBoard.Instance.attackMultiple *= 2;
        }
    }

    public override void Interrupt()
    {
        
    }
}
