using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1 : EnemyBase
{
    // Start is called before the first frame update
    public int maxPointsInHand = 15;
    void Start()
    {
        eA = GetComponentInParent<EnemyAchive>();
    }

    public override void OnTurnStart(){
        //Debug.Log("E0 Draw");
        TurnManager.Instance.EnemyTurn_draw();
    }

    public override void OnTurnDraw()
    {
        if (GamePointBoard.Instance.enemyCardPoints < maxPointsInHand)
        {
            //Debug.Log("E0 Drawed");
            eA.enemyHolder.DrawCard();
        }else{
            GamePointBoard.Instance.RecordSuspensionE();
        }
        TurnManager.Instance.EnemyTurn_end();
    }
    public override void OnPlayerDraw(){
        StartCoroutine(DrawCardIdentify());
    }

    IEnumerator DrawCardIdentify(){
        yield return new WaitForSeconds(0.5f);
        List<Card> playerCards = eA.playerHolder.cards;
        Debug.Log(playerCards[playerCards.Count - 1].suit);
        // 实现“闪电”技能，玩家抽到黑桃2-9就要多抽一张牌
        if(playerCards[playerCards.Count - 1].points < 10 &&
        playerCards[playerCards.Count - 1].points > 1&&
        playerCards[playerCards.Count - 1].suit == CardSuit.黑桃){
            Debug.Log("E1_skilled");
            eA.playerHolder.DrawCard();
        }
    }
}