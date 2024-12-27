using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDeck;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;

public class CardDack : MonoBehaviour
{
    public int numberOfCards = 52;
    public Text logDisplayText;
    public Text logDisplayText2;
    public bool isShuffled = false;
    public GameObject ShowPannel;
    //抽牌堆
    [SerializeField] public List<CardString> cardsDeck = new List<CardString>();
    //弃牌堆
    [SerializeField] public List<CardString> discardDeck = new List<CardString>();
    [SerializeField] public bool hasStartAnim = true;
    private static CardDack _instance;
    public AudioSource drawCard;
    public AudioSource shuffleCard;
    public static CardDack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardDack>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SkillPool");
                    _instance = obj.AddComponent<CardDack>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCardPoints();
    }


    //牌堆初始化
    void InitializeCardPoints()
    {
        List<String> point = new List<String> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        foreach (var suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (var str in point)
            {
                cardsDeck.Add(new CardString(str, (CardSuit)suit));
            }
        }
        //洗牌
        numberOfCards = 52;
        ShuffleCards(cardsDeck);
        shuffleCard.Play();
        isShuffled =true;
    }

    public void ShuffleCards(List<CardString> cardsPoint)
    {
        numberOfCards = 52;
        for (int i = cardsPoint.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            // Swap cardsPoint[i] with the element at randomIndex
            CardString temp = cardsPoint[i];
            cardsPoint[i] = cardsPoint[randomIndex];
            cardsPoint[randomIndex] = temp;
        }
        //随机选择其中不重复8张将其isTreasure为true
        int i1 = 0;
        while (i1 < 8)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardsPoint.Count);
            CardString temp = cardsPoint[randomIndex];
            if (temp.isTreasure == false)
            {
                temp.isTreasure = true;
                cardsPoint[randomIndex] = temp;
                i1++;
            }
            else
            {
                continue;
            }
        }
    }

    public CardString DrawCard()
    {
        drawCard.Play();
        numberOfCards -= 1;
        if (cardsDeck.Count == 0)
        {
            Debug.LogWarning("No cards left in the deck.");
            return new CardString("0", CardSuit.黑桃);
        }

        // 随机抽取一张牌
        int randomIndex = 0;
        CardString drawnCard = cardsDeck[randomIndex];

        // 从列表中移除这张牌
        cardsDeck.RemoveAt(randomIndex);

        return drawnCard;
    }

    //将弃牌堆的卡放入到抽牌堆
    public void RecoverDiscard()
    {
        InitializeCardPoints();
        discardDeck.Clear();
    }
    public void OnMouseEnter()
    {
        LogTreasureCards();
        ShowPannel.SetActive(true);
    }

    public void OnMouseExit()
    {
        logDisplayText.text = "";
        ShowPannel.SetActive(false);
    }

    private void LogTreasureCards()
    {
        int countPrinted = 0;
        foreach (CardString card in cardsDeck)
        {
            if (card.isTreasure)
            {
                string logMessage = $"卡堆中第{countPrinted + 1}张宝牌:{card.suit + card.point} 在牌堆中的位置是: {cardsDeck.IndexOf(card) + 1}";
                Debug.Log(logMessage);
                logDisplayText.text += logMessage + "\n"; // 更新UI显示
                countPrinted++;
                if (countPrinted >= 1)
                {
                    break;
                }
            }
        }
    }
    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logString.StartsWith("运筹帷幄"))
        {
            logDisplayText2.text = logString; // 更新UI显示，仅显示最新的日志信息 }
        }
    }
}



