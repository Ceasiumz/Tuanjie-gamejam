using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    [NonSerialized]public int health = 100;
    [NonSerialized]public int maxPointsInHand = 15;
    protected EnemyAchive eA;
    void Start()
    {
        eA = GetComponentInParent<EnemyAchive>();
    }

    public virtual void OnTurnStart(){
        Debug.Log("Enemy Turn Start");
    }

    public virtual void OnPlayerStart(){
        Debug.Log("Player Turn Start");
    }
    public virtual void OnTurnDraw(){
        Debug.Log("Enemy Turn Draw");
        eA.enemyHolder.DrawCard();
    }
    public virtual void OnPlayerDraw(){
        Debug.Log("Player Turn Draw");
    }
    public virtual void OnTurnSuspend(){
        TurnManager.Instance.EnemyTurn_Start
        .AddListener(TurnManager.Instance.EnemyTurn_end);
        Debug.Log("Enemy Turn Suspend");
    }
    public virtual void OnPlayerSuspend(){
        Debug.Log("Player Turn Suspend");
    }
        public virtual void OnTurnEnd(){
        Debug.Log("Enemy Turn End");
    }
    public virtual void OnPlayerEnd(){
        Debug.Log("Player Turn End");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected virtual void CowardDraw(){
        StartCoroutine(WaitingCowardDraw());
    }
    IEnumerator WaitingCowardDraw(){
        if (GamePointBoard.Instance.enemyCardPoints < maxPointsInHand)
        {
            //Debug.Log("E0 Drawed");
            //eA.enemyHolder.DrawCard();
            yield return StartCoroutine(eA.enemyHolder.WaitForInstantiationAndProcessCard(0));
        }else{
            TurnManager.Instance.EnemyTurn_suspend();
        }
        TurnManager.Instance.EnemyTurn_end();
    }
}
