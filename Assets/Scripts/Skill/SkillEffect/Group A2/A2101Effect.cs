using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2101Effect")]
public class A2101Effect : BaseEffect
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
        if (GamePointBoard.Instance.playerCardPoints == 21 && AllyPoint.Instance.holder.cards.Count == 5)
        {
            GamePointBoard.Instance.attackAddition += GamePointBoard.Instance.enemyCurrentHealth;
        }
    }

    public override void Interrupt()
    {
        
    }
}
