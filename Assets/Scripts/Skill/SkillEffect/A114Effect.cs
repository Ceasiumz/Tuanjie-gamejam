using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A114Effect")]
public class A114Effect : BaseEffect
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
        GamePointBoard.Instance.currentHealth += Mathf.RoundToInt(GamePointBoard.Instance.maxHealth * 0.2f);
    }

    public override void Interrupt()
    {
        
    }
}
