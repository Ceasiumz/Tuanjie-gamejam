using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6 : EnemyBase
{
    // Start is called before the first frame update
    public int rageCountMax = 0;
    void Start()
    {
        eA = GetComponentInParent<EnemyAchive>();
        DynamicEventBus.Subscribe("AfterPlayerAttackEvent", Enrage);
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
        RageCheck();
        //Debug.Log("Nowa");
    }

    public void Enrage()//受到伤害后，玩家下一个回合每张手牌的点数+1，这个效果最多叠加两次
    {
        Debug.Log("E6 Skilled");
        if (rageCountMax < 2)
        {
            rageCountMax++;
        }
    }

    public void RageCheck()//如果Card的rageCount小于RageMax，则使其点数加一
    {
        List<Card> playerCards = eA.playerHolder.cards;
        foreach (Card card in playerCards){
            while (card.rageCount < rageCountMax)
            {
                card.rageCount++;
                card.points++;
                int nowPoint = card.points;
                card.name = nowPoint.ToString();
            }
        }
    }
    private void OnDisable() {
        
        DynamicEventBus.Unsubscribe("AfterPlayerAttackEvent", Enrage);
    }
}
