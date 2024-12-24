using System.Collections;
using System.Collections.Generic;
using CardDeck;
using UnityEngine;

public class E4 : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] int Ehealth;
    [SerializeField] int EmaxPointsInHand;
    [SerializeField] BaseSkill markedSkill;
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
        //每2个回合，玩家发动的第一个主动技能总是无效
        if (TurnManager.Instance.turnCount % 2 == 0)
        {
            Debug.Log("E4 skilled");
            //使得玩家发动的第一个主动技能总是无效
            //markedskill = true;
        }
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
  

}
