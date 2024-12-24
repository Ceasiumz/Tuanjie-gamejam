using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A107Effect")]
public class A107Effect : BaseEffect
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
        
    }

    public void SkillExecute(List<Card> cards)
    {
        GamePointBoard.Instance.HideCard(cards[0]);
        SkillPool.Instance.ReturnSkillFromPlayerSkill("A107");
        SkillPool.Instance.RemovePlayerSkillByID("A107");
        SkillPool.Instance.AddPlayerSkill(SkillPool.Instance.FindSkillByID("A107-1"));
        DynamicEventBus.Unsubscribe<List<Card>>("SkillExecute", SkillExecute);
    }
    
    public override void ImmediateTrigger()
    {
        
    }

    public override void Interrupt()
    {
        DynamicEventBus.Unsubscribe<List<Card>>("SkillExecute", SkillExecute);
    }
}
