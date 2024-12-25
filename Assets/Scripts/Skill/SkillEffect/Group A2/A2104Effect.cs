using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2104Effect")]
public class A2104Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("BeforePlayerCardPointSettle", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("BeforePlayerCardPointSettle", EventSkill);
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
        int recountCardPoint = 0;
        foreach (var card in AllyPoint.Instance.holder.cards)
        {
            
            
            card.points = Mathf.Max(0, card.points - 1);
            recountCardPoint += card.points;
        }
        GamePointBoard.Instance.playerCardPoints=recountCardPoint;
    }

    public override void Interrupt()
    {
        
    }
}
