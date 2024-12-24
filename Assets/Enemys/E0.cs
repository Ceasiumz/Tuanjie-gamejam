using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E0 : EnemyBase
{
    // Start is called before the first frame update
    
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
        CowardDraw();
    }
    public override void OnPlayerDraw(){
        //Debug.Log("Nowa");
    }
}
