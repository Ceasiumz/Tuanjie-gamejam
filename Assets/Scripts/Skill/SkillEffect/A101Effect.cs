using UnityEngine;


[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/A101Effect")]
public  class A101Effect:BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe<Card>("AfterDrawCardSettle", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe<Card>("AfterDrawCardSettle", EventSkill);
    }

    public void EventSkill(Card card) 
    {
            if (card.suit == CardSuit.红桃)
            {
                GamePointBoard.Instance.currentHealth += card.points;
                Debug.Log(" 抽取红桃 触发技能 点数"+card.points+"当前生命值"+GamePointBoard.Instance.currentHealth);
            }
    }

    public override void Execute()
    {
        
    }

    public override void ImmediateTrigger()
    {
        
    }
}