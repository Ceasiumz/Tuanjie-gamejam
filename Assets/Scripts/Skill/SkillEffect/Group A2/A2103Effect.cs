using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2103Effect")]
public class A2103Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("BeforeEnemyAttackEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("BeforeEnemyAttackEvent", EventSkill);
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }
    public void EventSkill()
    {
        int playerCardCount = AllyPoint.Instance.holder.cards.Count;
        int enemyCardCount= AllyPoint.Instance.ememyHolder.cards.Count;
        if (playerCardCount > enemyCardCount)
        {
            GamePointBoard.Instance.injuryReduction += (playerCardCount - enemyCardCount) *4;
        }
    }

    public override void Interrupt()
    {
        
    }
}
