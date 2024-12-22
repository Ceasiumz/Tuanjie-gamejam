using UnityEngine;


[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/Skill/A101Effect")]
public  class A101Effect:BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe<Card>("AfterDrawCardSettle", EEventTest);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe<Card>("AfterDrawCardSettle", EEventTest);
    }

    public void EEventTest(Card card)
    {
        if (card.suit == CardSuit.红桃)
        {
            GamePointBoard.Instance.currentHealth += card.points;
            Debug.Log(" 抽取红桃 触发技能 点数"+card.points+"当前生命值"+GamePointBoard.Instance.currentHealth);
        }
    }
    
}