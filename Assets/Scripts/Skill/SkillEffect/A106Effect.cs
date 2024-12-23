using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A106Effect")]
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
        DynamicEventBus.Subscribe<List<Card>>("SkillExecute", SkillExecute);
        
        
        
        // SkillPool.Instance.ReturnSkillFromPlayerSkill("A106");
        // SkillPool.Instance.RemovePlayerSkillByID("A106");
        
        
        //End Execute
        DynamicEventBus.Subscribe<List<Card>>("SkillExecute", SkillExecute);
    }

    public void SkillExecute(List<Card> cards)
    {
        AllyPoint.Instance.holder.cards.Add(cards[0]);
    }
    
    public override void ImmediateTrigger()
    {
        
    }
}
