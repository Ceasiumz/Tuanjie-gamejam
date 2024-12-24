using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/Normal/A107-1Effect")]
public class A107_1Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("RoundEndEvent",SkillEnd);
    }

    public override void unsubscribeEvent()
    {
        
    }

    public override void Execute()
    {
        AllyPoint.Instance.holder.cards.Add(GamePointBoard.Instance.GetHiddenCard()[0]);
        GamePointBoard.Instance.UpdateCardPoints(false, AllyPoint.Instance.holder.cards);
        SkillPool.Instance.RemovePlayerSkillByID("A107-1");
        DynamicEventBus.Unsubscribe("RoundEndEvent",SkillEnd);
    }
    
    public override void ImmediateTrigger()
    {
        
    }
    public void SkillEnd()
    {
        //对手死亡卡牌也没使用
        //消除保留卡牌
        GamePointBoard.Instance.destroyEvidence();
        //消除此技能
        SkillPool.Instance.RemovePlayerSkillByID("A107-1");
        DynamicEventBus.Unsubscribe("RoundEndEvent",SkillEnd);
    }

    public override void Interrupt()
    {
        
    }
}
