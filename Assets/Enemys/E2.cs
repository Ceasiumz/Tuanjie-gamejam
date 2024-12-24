using System.Collections;
using System.Collections.Generic;
using CardDeck;
using UnityEngine;

public class E2 : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] int Ehealth;
    [SerializeField] int EmaxPointsInHand;
    private void Awake()
    {
        health = Ehealth;
        maxPointsInHand = EmaxPointsInHand;
    }
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
       CowardDraw();
    }
    public override void OnPlayerDraw()
    {
        Debug.Log("E2_normal");
        StartCoroutine(DrawCardIdentify());
        GamePointBoard.Instance.UpdateCardPoints(false, eA.playerHolder.cards);
    }

    IEnumerator DrawCardIdentify()
    {
        //往牌库里塞一张草花二
        CardDack.Instance.cardsDeck.Insert(0, new CardString("2", CardSuit.梅花));
        yield return new WaitForSeconds(0.5f);
        // 实现“扰乱”技能，玩家抽牌后会获得一张草花2
        List<Card> playerCards = eA.playerHolder.cards;
        Debug.Log("E2_skilled");
        eA.playerHolder.DrawCard();
    }
}
