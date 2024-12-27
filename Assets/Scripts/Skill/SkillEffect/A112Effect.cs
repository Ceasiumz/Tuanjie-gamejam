using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A112Effect")]
public class A112Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("AfterPlayerAttackEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("AfterPlayerAttackEvent", EventSkill);
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }
    public void EventSkill()
    {
        GamePointBoard.Instance.currentHealth += 3+Mathf.RoundToInt(
            (GamePointBoard.Instance.attack + GamePointBoard.Instance.attackAddition) *
            GamePointBoard.Instance.attackMultiple * 0.2f);
    }

    public override void Interrupt()
    {
        
    }
}
