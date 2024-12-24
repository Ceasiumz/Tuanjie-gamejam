using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2107Effect")]
public class A2107Effect : BaseEffect
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
        if (cards[0].points >= cards[1].points)
        {
            cards[1].points=cards[0].points-cards[1].points;
            if(cards[1].points==1)
                cards[1].name = "A";
            else
                cards[1].name = cards[1].points.ToString();
        }else
        {
            cards[0].points=cards[1].points-cards[0].points;
            if (cards[0].points == 1)
            {
                cards[0].name = "A";
            }else
            {
                cards[0].name = cards[0].points.ToString();
            }
            
        }
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
