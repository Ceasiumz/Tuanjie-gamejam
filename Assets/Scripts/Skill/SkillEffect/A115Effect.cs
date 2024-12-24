using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A115Effect")]
public class A115Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        
    }

    public override void unsubscribeEvent()
    {
        
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        GamePointBoard.Instance.skillChouseNum = 2;
    }

    public override void Interrupt()
    {
        
    }
}
