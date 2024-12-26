using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3101Effect")]
public class A3101Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        Debug.Log("技能事件订阅 A3101");
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
        if (GamePointBoard.Instance.playerCardPoints == 21)
        {
            Debug.Log("触发技能A3101 伤害倍率翻倍");
            GamePointBoard.Instance.attackMultiple *= 2;
        }
    }

    public override void Interrupt()
    {
        
    }
}
