using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3104Effect")]
public class A3104Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        
    }

    public override void unsubscribeEvent()
    {
        
    }

    public override void Execute()
    {
        DynamicEventBus.Subscribe<List<Card>>("SkillExecute", SkillExecute);
    }

    public void SkillExecute(List<Card> cards)
    {
        AllyPoint.Instance.holder.cards.Remove(cards[0]);
        AllyPoint.Instance.holder.DrawCard();
        DynamicEventBus.Unsubscribe<List<Card>>("SkillExecute", SkillExecute);
    }
    public override void Interrupt()
    {
        
    }

    public override void ImmediateTrigger()
    {
        DynamicEventBus.Unsubscribe<List<Card>>("SkillExecute", SkillExecute);
    }
}
