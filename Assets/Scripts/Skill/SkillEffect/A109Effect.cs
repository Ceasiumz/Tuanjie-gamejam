using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A109Effect")]
public class A109Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        
    }

    public override void unsubscribeEvent()
    {
        
    }

    public override void Execute()
    {
        GamePointBoard.Instance.attackMultiple *= 2;
        GamePointBoard.Instance.injuryMultiple *= 2;
    }

    public override void ImmediateTrigger()
    {
        
    }
}
