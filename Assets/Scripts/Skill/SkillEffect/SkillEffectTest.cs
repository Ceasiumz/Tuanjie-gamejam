using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/Skill/SkillEffectTest")]
public  class SkillEffectTest:BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe<Card>("testEventBus", EEventTest);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe<Card>("testEventBus", EEventTest);
    }

    public void EEventTest(Card card)
    {
        Debug.Log("事件系统 输出抽取到的卡牌花色和点数"+card.suit+card.name);
    }
    
}