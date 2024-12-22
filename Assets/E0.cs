using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E0 : EnemyBase
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
            TurnManager.Instance.EnemyTurn_end();
        }else{
            GamePointBoard.Instance.RecordSuspension(true);
        }
    }
    public override void OnPlayerDraw(){
        Debug.Log("Nowa");
    }
}
