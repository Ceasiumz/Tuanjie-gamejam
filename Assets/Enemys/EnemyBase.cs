using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public int maxPointsInHand = 15;
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
}
