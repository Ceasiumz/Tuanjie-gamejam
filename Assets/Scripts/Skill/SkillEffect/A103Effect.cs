using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A103Effect")]
public class A103Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("PlayerDeadEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("PlayerDeadEvent", EventSkill);
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }
    public void EventSkill()
    {
        GamePointBoard.Instance.currentHealth = 1;
        SkillPool.Instance.RemovePlayerSkillByID("A103");
    }

    public override void Interrupt()
    {
        
    }
}
