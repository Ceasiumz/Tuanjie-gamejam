using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2 : EnemyBase
{
    // Start is called before the first frame update
    private void Awake()
    {
        health = 88;
        maxPointsInHand = 15;
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
        if (GamePointBoard.Instance.enemyCardPoints < maxPointsInHand)
        {
            //Debug.Log("E0 Drawed");
            eA.enemyHolder.DrawCard();
        }
        else
        {
            GamePointBoard.Instance.RecordSuspensionE();
        }
        TurnManager.Instance.EnemyTurn_end();
    }
    public override void OnPlayerDraw()
    {
        StartCoroutine(DrawCardIdentify());
    }

    IEnumerator DrawCardIdentify()
    {
        //TODO:往牌库里塞一张草花二
        // 实现“扰乱”技能，玩家抽牌后会获得一张草花2
        yield return new WaitForSeconds(0.5f);
        List<Card> playerCards = eA.playerHolder.cards;
        Debug.Log(playerCards[playerCards.Count - 1].suit);
        Debug.Log("E2_skilled");
        eA.playerHolder.DrawCard();

    }
}
