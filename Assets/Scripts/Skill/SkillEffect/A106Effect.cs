using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/A106Effect")]
//TODO:具体实现方法待定
public class A106Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        
    }

    public override void unsubscribeEvent()
    {
        
    }

    public override void Execute()
    {

        // SkillPool.Instance.ReturnSkillFromPlayerSkill("A106");
        // SkillPool.Instance.RemovePlayerSkillByID("A106");
    }
    
    public override void ImmediateTrigger()
    {
        
    }
}
