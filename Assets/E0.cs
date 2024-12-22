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

    public override void OnTurnDraw()
    {
        if (GamePointBoard.Instance.enemyCardPoints < maxPointsInHand)
        {
            eA.enemyHolder.DrawCard();
        }else{
            GamePointBoard.Instance.isEnemySuspension = true;
        }
    }
    public override void OnPlayerDraw(){
        Debug.Log("Nowa");
    }
}
