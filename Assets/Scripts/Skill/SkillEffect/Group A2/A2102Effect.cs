using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2102Effect")]
public class A2102Effect : BaseEffect
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
        GamePointBoard.Instance.attack +=
            Mathf.Max(0, AllyPoint.Instance.holder.cards.Count - AllyPoint.Instance.ememyHolder.cards.Count) * 3;
    }

    public override void Interrupt()
    {
        
    }
}
