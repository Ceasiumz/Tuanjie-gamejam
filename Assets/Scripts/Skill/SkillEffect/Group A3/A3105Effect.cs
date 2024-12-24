using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/G3/A3105Effect")]
public class A3105Effect : BaseEffect
{
    public override void subscribeEvent()
    {
        DynamicEventBus.Subscribe("PlayerTurnStartEvent", EventSkill);
    }

    public override void unsubscribeEvent()
    {
        DynamicEventBus.Unsubscribe("PlayerTurnStartEvent", EventSkill);
    }

    public override void Execute()
    {
        DynamicEventBus.Subscribe<List<Card>>("SkillExecute", SkillExecute);
    }

    public override void ImmediateTrigger()
    {
        
    }
    public void SkillExecute(List<Card> cards)
    {
        bool temTreasure = CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count - 1].isTreasure;
        CardSuit temsuit=CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count - 1].suit;
        string tempoint=CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count - 1].point;

        CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count - 1].isTreasure = cards[0].isTreasure;
        CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count - 1].suit = cards[0].suit;
        CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count - 1].point = cards[0].name;
        
        cards[0].isTreasure = temTreasure;
        cards[0].suit = temsuit;
        cards[0].name = tempoint;
        switch (tempoint)
        {
            case "A":
                cards[0].points = 1;
                break;
            case "J":
                cards[0].points = 11;
                break;
            case "Q":
                cards[0].points = 12;
                break;
            case "K":
                cards[0].points = 13;
                break;
            default:
                if (int.TryParse(tempoint, out int parsedPoint))
                {
                    cards[0].points = parsedPoint;
                }
                else
                {
                    Debug.LogError($"无法解析牌面点数 {tempoint}，设置默认点数为0 技能A3105错误");
                    cards[0].points = 0;
                }
                break;
        }
        
        
        GamePointBoard.Instance.UpdateCardPoints(false, AllyPoint.Instance.holder.cards);
    }

    public override void Interrupt()
    {
        DynamicEventBus.Unsubscribe<List<Card>>("SkillExecute", SkillExecute);
    }
    public void EventSkill()
    {
        Debug.Log("牌堆最后一张牌为"+CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count-1].suit+CardDack.Instance.cardsDeck[CardDack.Instance.cardsDeck.Count-1].point);
    }
}
