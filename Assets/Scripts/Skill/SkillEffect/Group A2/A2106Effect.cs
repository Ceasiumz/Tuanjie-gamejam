using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2106Effect")]
public class A2106Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("AfterPlayerAttackEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("AfterPlayerAttackEvent", EventSkill);
    }

    public void EventSkill()
    {
        int playerCardCount= AllyPoint.Instance.holder.cards.Count;
        int enemyCardCount= AllyPoint.Instance.ememyHolder.cards.Count;
        if (playerCardCount > enemyCardCount)
        {
            GamePointBoard.Instance.currentHealth+=(playerCardCount-enemyCardCount)*2;
        }
    }
    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }

    public override void Interrupt()
    {
        
    }
}
