using System.Collections;
using System.Collections.Generic;
using CardDeck;
using Unity.VisualScripting;
using UnityEngine;

public class E7 : EnemyBase
{
    // Start is called before the first frame update
    public Card SatisfiedCard;
    void Start()
    {
        eA = GetComponentInParent<EnemyAchive>();
    }

    public override void OnTurnStart()
    {
        //Debug.Log("E0 Draw");
        TurnManager.Instance.EnemyTurn_draw();
    }

    public override void OnTurnDraw()
    {
        //CowardDraw();
        //只要玩家手牌中存在至少一张不会使自己爆牌的牌，则从玩家手中获得那些牌中点数最大的牌
        List<Card> playerCards = eA.playerHolder.cards;
        List<Card> enemyCards = eA.enemyHolder.cards;
        SatisfiedCard = null;
        foreach (Card card in playerCards)
        {
            if (card.points + GamePointBoard.Instance.enemyCardPoints <= maxPointsInHand)
            {
                SatisfiedCard = card;
            }
            if (SatisfiedCard != null)
            {
                break;
            }
        }
        if (SatisfiedCard != null)
        {
            Debug.Log("E7 Skilled");
        }
        else
        {
            TurnManager.Instance.EnemyTurn_end();
            return;
        }
        StartCoroutine(SwapAnim());
        
    }
    IEnumerator SwapAnim(){
        CardDack.Instance.hasStartAnim = false;
        yield return StartCoroutine(SatisfiedCard.cardVisual.SwapWithEnemyAnim());
        StartCoroutine(CardIdentifyThenDestroy());
    }
    IEnumerator CardIdentifyThenDestroy()
    {
        //往牌库里塞一张SatisfiedCard
        CardDack.Instance.cardsDeck.Insert(0, new CardString(SatisfiedCard.name, SatisfiedCard.suit));
        eA.playerHolder.DestroyCard(SatisfiedCard);
        yield return StartCoroutine(eA.enemyHolder.WaitForInstantiationAndProcessCard(0));
        //eA.enemyHolder.DrawCard();
        CardDack.Instance.hasStartAnim = true;
        TurnManager.Instance.EnemyTurn_end();
    }
    public override void OnPlayerDraw()
    {
        //Debug.Log("Nowa");
    }

    protected override void CowardDraw()
    {
        StartCoroutine(WaitingCowardDraw());
    }
    IEnumerator WaitingCowardDraw()
    {
        if (GamePointBoard.Instance.enemyCardPoints < maxPointsInHand)
        {
            yield return StartCoroutine(eA.enemyHolder.WaitForInstantiationAndProcessCard(0));
        }
        else
        {
            TurnManager.Instance.EnemyTurn_suspend();
        }
    }
}
