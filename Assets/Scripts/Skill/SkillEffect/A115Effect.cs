using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A115Effect")]
public class A115Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("RoundEndEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("RoundEndEvent", EventSkill);
    }

    public override void Execute()
    {
        
    }

    public void EventSkill()
    {
        SkillPool.Instance.ReturnSkillFromPlayerSkill("A115");
        SkillPool.Instance.RemovePlayerSkillByID("A115");
    }
    
    public override void ImmediateTrigger()
    {
        Debug.Log("A115发动");
        GamePointBoard.Instance.skillChouseNum = 2;
    }

    public override void Interrupt()
    {
        
    }
}
