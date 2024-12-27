using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class E1 : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] int Ehealth;
    [SerializeField] int EmaxPointsInHand;
    [SerializeField] int Eattack;
    DialogueTreeController startDialogue;
    DialogueTreeController dieDialogue;
    private void Awake()
    {
        health = Ehealth;
        maxPointsInHand = EmaxPointsInHand;
        attack = Eattack;
    }
    void Start()
    {
        dieDialogue = GetComponent<DialogueTreeController>();
        startDialogue = GetComponent<DialogueTreeController>();
        eA = GetComponentInParent<EnemyAchive>();
        startDialogue.StartDialogue();
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
        StartCoroutine(DrawCardIdentify());
        //GamePointBoard.Instance.UpdateCardPoints(false, eA.playerHolder.cards);
    }

    IEnumerator DrawCardIdentify()
    {
        yield return new WaitForSeconds(0.5f);
        List<Card> playerCards = eA.playerHolder.cards;
        //Debug.Log(playerCards[playerCards.Count - 1].suit);
        // 实现“闪电”技能，玩家抽到黑桃2-9就要多抽一张牌
        if (playerCards[playerCards.Count - 1].points < 10 &&
        playerCards[playerCards.Count - 1].points > 1 &&
        playerCards[playerCards.Count - 1].suit == CardSuit.黑桃)
        {
            Debug.Log("E1_skilled");
            eA.playerHolder.DrawCard();
        }
    }
}
