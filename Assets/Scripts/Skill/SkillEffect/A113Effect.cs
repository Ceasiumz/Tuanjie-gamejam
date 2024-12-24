using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A113Effect")]
public class A113Effect : BaseEffect
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
        GamePointBoard.Instance.attack += (2 + Mathf.RoundToInt(GamePointBoard.Instance.attack * 0.1f));
    }

    public override void Interrupt()
    {
        
    }
}
