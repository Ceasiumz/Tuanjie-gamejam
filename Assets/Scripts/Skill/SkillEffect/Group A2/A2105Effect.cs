using System.Collections;
using System.Collections.Generic;
using CardDeck;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G2/A2105Effect")]
public class A2105Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        
    }

    public override void unsubscribeEvent()
    {
        
    }

    public override void Execute()
    {
        //TODO:跳过抽牌阶段
        //生成随机数1-6
        int dice = Random.Range(1, 7);
        
        CardDack.Instance.cardsDeck.Insert(0, new CardString("2", CardSuit.方块));
        AllyPoint.Instance.holder.DrawCard();
        
    }

    public override void ImmediateTrigger()
    {
        
    }

    public override void Interrupt()
    {
        
    }
}
