using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDack : MonoBehaviour
{
    //抽牌堆
     public List<String> cardsPoint;
     //弃牌堆
     public List<String> discardDeck;
    // Start is called before the first frame update
    void Start()
    {
        InitializeCardPoints();
    }


    //牌堆初始化
    void InitializeCardPoints()
    {
        cardsPoint = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "J", "Q","A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "J", "Q","A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "J", "Q","A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "J", "Q" };
        //洗牌
        ShuffleCards(cardsPoint);
    }
    
    public void ShuffleCards(List<String> cardsPoint)
    {
        for (int i = cardsPoint.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            // Swap cardsPoint[i] with the element at randomIndex
            string temp = cardsPoint[i];
            cardsPoint[i] = cardsPoint[randomIndex];
            cardsPoint[randomIndex] = temp;
        }
    }
    
        public string DrawCard()
    {
        if (cardsPoint.Count == 0)
        {
            Debug.LogWarning("No cards left in the deck.");
            return null;
        }

        // 随机抽取一张牌
        int randomIndex = 0;
        string drawnCard = cardsPoint[randomIndex];

        // 从列表中移除这张牌
        cardsPoint.RemoveAt(randomIndex);

        return drawnCard;
    }
        
        //将弃牌堆的卡放入到抽牌堆
        public void RecoverDiscard()
    {
        cardsPoint.AddRange(discardDeck);
        discardDeck.Clear();
    }
}
