using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using CardDeck;
using UnityEngine.Events;

public class HorizontalCardHolder : MonoBehaviour
{
    public int point = 0;
    [SerializeField] private Card selectedCard;
    [SerializeField] private Card hoveredCard;
    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 7;
    public List<Card> cards = new List<Card>();
    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;
    public UnityEvent<Card> DrawEvent;

    public GameObject DrawButton;
    [SerializeField] private CardDack cardDack;

    public float discardWaitTime = 0.5f;
    public bool isEnemy;
    

    // private static HorizontalCardHolder _instance;
    // private static HorizontalCardHolder _instance2;
    // public static HorizontalCardHolder PlayerInstance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = FindObjectOfType<HorizontalCardHolder>();
    //             if (_instance == null)
    //             {
    //                 GameObject obj = new GameObject("HorizontalCardHolder");
    //                 _instance = obj.AddComponent<HorizontalCardHolder>();
    //             }
    //         }
    //         return _instance;
    //     }
    // }
    //
    // private void Awake()
    // {
    //     if (_instance == null)
    //     {
    //         _instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else if (_instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    
    
    
    private void LateUpdate()
    {
    }

    public void DrawCard()
    {
        // 使用协程来等待异步实例化操作完成，避免死循环占用资源
        StartCoroutine(WaitForInstantiationAndProcessCard(0));
    }

    private IEnumerator WaitForInstantiationAndProcessCard(int hidenFlag)
    {
        var op = InstantiateAsync(slotPrefab, transform);
        yield return op;

        if (op.isDone)
        {
            GameObject cardObject = op.Result[0];
            Card card = cardObject.GetComponentInChildren<Card>();
            cards.Add(card);
            //
            if (hidenFlag == 1)
            {
                card.cardVisual.sprite.color = Color.black;
                card.isHiden = true;
            }
            // 抽取卡牌及相关逻辑处理
            ProcessDrawnCard(card);

            // 注册事件监听器
            RegisterCardEventListeners(card);
            if (gameObject.tag == "Enemy")
            {
                card.isEnemy = true;
                card.isEnemyEvent?.Invoke(card);
            }

            // 触发抽牌事件
            DrawEvent?.Invoke(card);

            // 等待一小段时间后更新卡牌视觉相关逻辑
            yield return UpdateCardVisual();
        }
        else
        {
            Debug.LogError("实例化卡牌失败！");
            // 可以根据实际情况考虑进一步的错误处理逻辑，比如重试等
        }
    }

    private void ProcessDrawnCard(Card card)
    {
        //从牌库抽取卡牌
        //判断抽排堆是否为空
        if (cardDack.cardsPoint.Count == 0)
        {
            cardDack.RecoverDiscard();
            cardDack.ShuffleCards(cardDack.cardsPoint);
        }
        CardString cardStruct = cardDack.DrawCard();
        string cardPoint = cardStruct.point;
        Debug.Log("抽取点数" + cardPoint);

        // 根据抽取的牌面设置点数
        switch (cardPoint)
        {
            case "A":
                card.points = 1;
                break;
            case "J":
                card.points = 11;
                break;
            case "Q":
                card.points = 12;
                break;
            case "K":
                card.points = 13;
                break;
            default:
                if (int.TryParse(cardPoint, out int parsedPoint))
                {
                    card.points = parsedPoint;
                }
                else
                {
                    Debug.LogError($"无法解析牌面点数 {cardPoint}，设置默认点数为0");
                    card.points = 0;
                }
                break;
        }

        card.suit = cardStruct.suit;
        card.CardRename(card);

        //测试代码
        //技能触发 抽卡后触发技能 传入参数为抽取到的卡牌
        DynamicEventBus.Publish("AfterDrawCardSettle", card);
        
        GamePointBoard.Instance.UpdateCardPoints(isEnemy, cards);

        // 可以考虑在这里统一判断抽牌堆是否为空，而不是每次抽牌都判断，减少重复操作
        // 示例如下（具体逻辑可能需根据实际情况微调）：
        if (cardDack.cardsPoint.Count == 0)
        {
            cardDack.RecoverDiscard();
            cardDack.ShuffleCards(cardDack.cardsPoint);
        }
    }

    private void RegisterCardEventListeners(Card card)
    {
        AddCardEventListeners(card);
    }

    private IEnumerator UpdateCardVisual()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].cardVisual != null)
                cards[i].cardVisual.UpdateIndex(transform.childCount);
        }
    }
    private void Awake()
    {
        //TurnManager.Instance.PlayerTurn_Start.AddListener();
        if (gameObject.tag != "Enemy")
        {
            TurnManager.Instance.PlayerTurn_Draw.AddListener(DrawCard);
        }else{
            //TurnManager.Instance.EnemyTurn_Draw.AddListener(DrawCard);
        }
    }
    void Start()
    {
        //DrawEvent.AddListener(DrawOutTest);
        cards = GetComponentsInChildren<Card>().ToList();
        for (int i = 0; i < cardsToSpawn; i++)
        {
            StartCoroutine(WaitForInstantiationAndProcessCard(1));
        }

        rect = GetComponent<RectTransform>();

        StartCoroutine(UpdateCardVisual());


    }

    private void AddCardEventListeners(Card card)
    {
        card.PointerEnterEvent.AddListener(CardPointerEnter);
        card.PointerExitEvent.AddListener(CardPointerExit);
        card.BeginDragEvent.AddListener(BeginDrag);
        card.EndDragEvent.AddListener(EndDrag);
    }

    private void BeginDrag(Card card)
    {
        selectedCard = card;
    }

    void EndDrag(Card card)
    {
        if (selectedCard == null)
            return;

        selectedCard.transform.DOLocalMove(selectedCard.selected ? new Vector3(0, selectedCard.selectionOffset, 0) : Vector3.zero, tweenCardReturn ? .15f : 0).SetEase(Ease.OutBack);

        rect.sizeDelta += Vector2.right;
        rect.sizeDelta -= Vector2.right;

        selectedCard = null;
    }

    void CardPointerEnter(Card card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        hoveredCard = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DestroyCard(hoveredCard);
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Card card in cards)
            {
                card.Deselect();
            }
        }

        if (selectedCard == null)
            return;

        if (isCrossing)
            return;

        for (int i = 0; i < cards.Count; i++)
        {

            if (selectedCard.transform.position.x > cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() < cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }

            if (selectedCard.transform.position.x < cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() > cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    private void DestroyCard(Card card)
    {
        if (card != null)
        {
            cardDack.discardDeck.Add( new CardString(card.name, card.suit));
            // 更安全地从集合中移除卡牌，可考虑倒序遍历避免索引问题
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                if (cards[i] == card)
                {
                    Destroy(cards[i].transform.parent.gameObject);
                    cards.RemoveAt(i);
                    break;
                }
            }
        }
    }

    void Swap(int index)
    {
        isCrossing = true;

        Transform focusedParent = selectedCard.transform.parent;
        Transform crossedParent = cards[index].transform.parent;

        cards[index].transform.SetParent(focusedParent);
        cards[index].transform.localPosition = cards[index].selected ? new Vector3(0, cards[index].selectionOffset, 0) : Vector3.zero;
        selectedCard.transform.SetParent(crossedParent);

        isCrossing = false;

        if (cards[index].cardVisual == null)
            return;

        bool swapIsRight = cards[index].ParentIndex() > selectedCard.ParentIndex();
        cards[index].cardVisual.Swap(swapIsRight ? -1 : 1);

        //Updated Visual Indexes
        foreach (Card card in cards)
        {
            card.cardVisual.UpdateIndex(transform.childCount);
        }
    }

    public void DiscardHandCard()
    {
        TurnManager.Instance.PlayerTurn_start();
        StartCoroutine(WaitToDiscardHandCard());
    }

    private IEnumerator WaitToDiscardHandCard()
    {
        if (DrawButton != null)
        {
            DrawButton.SetActive(false);
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(discardWaitTime);
                DestroyCard(cards[i]);
            }
            DrawButton.SetActive(true);
        }
        else
        {
            Debug.Log("DrawButton已隐藏");
        }
        for (int i = cards.Count - 1; i >= 0; i--)
            {
                DestroyCard(cards[i]);
            }
    }

    public void DiscoverCardDeck()//重置牌组并洗牌
    {
        cardDack.RecoverDiscard();
        cardDack.ShuffleCards(cardDack.cardsPoint);
    }
}