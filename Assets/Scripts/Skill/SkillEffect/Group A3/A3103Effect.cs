using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3103Effect")]
public class A3103Effect : BaseEffect
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
        bool haveA=false;
        bool have9=false;
        foreach (Card card in AllyPoint.Instance.holder.cards)
        {
            if (card.name=="9")
            {
                have9 = true;
            }
            if (card.name=="A")
            {
                haveA = true;
            }
        }
        //当同为真或同为假时
        if (!(haveA ^ have9))
        {
            GamePointBoard.Instance.attackMultiple *= 2;
        }

    }



    public override void Interrupt()
    {
            
    }
}
