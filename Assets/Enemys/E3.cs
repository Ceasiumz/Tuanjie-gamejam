using System.Collections;
using System.Collections.Generic;
using CardDeck;
using UnityEngine;

public class E3 : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] int Ehealth;
    [SerializeField] int EmaxPointsInHand;
    [SerializeField] int Eattack;
    [SerializeField] Card markedCard;
    private void Awake()
    {
        health = Ehealth;
        maxPointsInHand = EmaxPointsInHand;
        attack = Eattack;
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
        CowardDraw();
    }
    public override void OnPlayerDraw()
    {
        Debug.Log("E2_normal");
        StartCoroutine(DrawCardIdentify());
        GamePointBoard.Instance.UpdateCardPoints(false, eA.playerHolder.cards);
    }

    IEnumerator DrawCardIdentify()
    {
        yield return new WaitForSeconds(0.5f);
        List<Card> playerCards = eA.playerHolder.cards;
        if (markedCard != null && playerCards.Count > 1)
        {
            Debug.Log("E3_skilled");
            eA.playerHolder.DestroyCard(markedCard);
        }
        markedCard = CardMark(playerCards);
    }

    Card CardMark(List<Card> playerCards)
    {//randomly mark a card in player's hand
        if (playerCards.Count > 1)
        {
            int index = Random.Range(1, playerCards.Count);
            playerCards[index].cardVisual.sprite.color = Color.red;
            return playerCards[index];
        }
        else
        {
            return null;
        }
    }
}
