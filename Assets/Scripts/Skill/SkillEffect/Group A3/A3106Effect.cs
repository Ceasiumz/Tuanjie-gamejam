using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3106Effect")]
public class A3106Effect : BaseEffect
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
        int count = 0;
        foreach (var card in AllyPoint.Instance.holder.cards)
        {
            if (card.name == "A" || card.name == "J" || card.name == "Q" || card.name == "K")
            {
                count++;
            }
        }
        GamePointBoard.Instance.attack+=6*count;
        GamePointBoard.Instance.currentHealth+=8*count;

    }
    
    public override void Interrupt()
    {
            
    }
}
