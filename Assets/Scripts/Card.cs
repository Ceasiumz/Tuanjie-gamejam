
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public bool isEnemy;
    public int rageCount = 0;
    public UnityEvent<Card> isEnemyEvent;
    public bool isHiden;
    private Canvas canvas;
    private Image imageComponent;
    [SerializeField] private bool instantiateVisual = true;
    private VisualCardsHandler visualHandler;
    private Vector3 offset;

    [Header("Movement")]
    [SerializeField] private float moveSpeedLimit = 50;

    [Header("Selection")]
    public bool selected;
    public float selectionOffset = 50;
    private float pointerDownTime;
    private float pointerUpTime;

    public int points = 0;
    public CardSuit suit;
    public bool isTreasure;

    [Header("Visual")]
    [SerializeField] private GameObject cardVisualPrefab;
    [HideInInspector] public CardVisual cardVisual;

    [Header("States")]
    public bool isHovering;
    public bool isDragging;
    [HideInInspector] public bool wasDragged;

    [Header("Events")]
    public UnityEvent<Card> PointerEnterEvent;
    public UnityEvent<Card> PointerExitEvent;
    public UnityEvent<Card, bool> PointerUpEvent;
    public UnityEvent<Card> PointerDownEvent;
    public UnityEvent<Card> BeginDragEvent;
    public UnityEvent<Card> EndDragEvent;

    public UnityEvent<Card> IsAceEvent;
    public UnityEvent<Card, bool> SelectEvent;
    public int startran = 1;


    void Start()// spawn card with visual sprite and random points
    {
        canvas = GetComponentInParent<Canvas>();
        imageComponent = GetComponent<Image>();

        if (!instantiateVisual)
            return;

        visualHandler = FindObjectOfType<VisualCardsHandler>();
        cardVisual = Instantiate(
        cardVisualPrefab,
        visualHandler ? visualHandler.transform : CardDack.Instance.transform)
        .GetComponent<CardVisual>();
        //cardVisual.transform.localPosition =  CardDack.Instance.transform.localPosition;
        cardVisual.Initialize(this);
        // if (points == 0)
        //     points = Random.Range(startran, 14);
        // CardRename(this);
        //Debug.Log("Card points: " + points);
        postPoints();
        
    }

    public void CardRename(Card card)
    {
        switch (card.points)
        {
            case 1: card.name = "A"; card.points = 11; break;
            case 11: card.name = "J"; card.points = 10; break;
            case 12: card.name = "Q"; card.points = 10; break;
            case 13: card.name = "K"; card.points = 10; break;
            default: card.name = card.points.ToString(); break;
        }
    }


    void postPoints()//special points effects
    {
        switch (points)
        {
            case 1: this.IsAceEvent.AddListener(IsAce); break;
            case 11: //points = 10; break;
            case 12: //points = 10; break;
            case 13: //points = 10; break;
            default: return;
        }
    }

    void IsAce(Card card)
    {//if card is ace, change points to 11 or 1
        if (card.points == 1 && AllyPoint.Instance.allyPoints <= 10)
        {
            card.points = 11;
        }
        else if (card.points == 11)
        {// if out of 21, change points to 1
            if (AllyPoint.Instance.allyPoints > AllyPoint.Instance.lim)
            {
                card.points = 1;
            }
        }
    }
    void Update()
    {
        ClampPosition();

        if (isDragging)
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }

        IsAceEvent.Invoke(this);
    }

    void ClampPosition()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2)transform.position;
        isDragging = true;
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        imageComponent.raycastTarget = false;

        wasDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);
        isDragging = false;
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
        imageComponent.raycastTarget = true;

        StartCoroutine(FrameWait());

        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            wasDragged = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnterEvent.Invoke(this);
        isHovering = true;
        if (isHiden && !isEnemy)
        {
            cardVisual.sprite.sprite = cardVisual.cardFace.face;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExitEvent.Invoke(this);
        isHovering = false;
        if (isHiden)
        {
            cardVisual.sprite.sprite = cardVisual.cardFace.back;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        PointerDownEvent.Invoke(this);
        pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUpTime = Time.time;

        PointerUpEvent.Invoke(this, pointerUpTime - pointerDownTime > .2f);

        if (pointerUpTime - pointerDownTime > .2f)
            return;

        if (wasDragged)
            return;

        selected = !selected;
        SelectEvent.Invoke(this, selected);

        if (MouseManager.Instance.isReleaseSkill)
        {
            if (isEnemy)
            {
                if (!MouseManager.Instance.CanSelectEnemyCard)
                {
                    return;
                }
            }
            if (selected)
            {// 如果选中了卡牌，就把卡牌加入mouseManager里的selectedCards列表
             //如果是释放技能就把卡牌加入mouseManager里的selectedCards列表
                if (MouseManager.Instance.selectedCards.Count < MouseManager.Instance.selectCardNum)
                {
                    transform.localPosition += cardVisual.transform.up * selectionOffset;
                    if (true)
                    {
                        MouseManager.Instance.selectedCards.Add(this);
                    }
                }
            }
            else// 如果取消选中卡牌，就把卡牌从mouseManager里的selectedCards列表移除
            {
                transform.localPosition = Vector3.zero;
                MouseManager.Instance.selectedCards.Remove(this);
            }
        }
    }

    public void Deselect()
    {
        if (selected)
        {
            selected = false;
            if (selected)
                transform.localPosition += (cardVisual.transform.up * 50);
            else
                transform.localPosition = Vector3.zero;
        }
    }


    public int SiblingAmount()
    {
        return transform.parent.CompareTag("Slot") ? transform.parent.parent.childCount - 1 : 0;
    }

    public int ParentIndex()
    {
        return transform.parent.CompareTag("Slot") ? transform.parent.GetSiblingIndex() : 0;
    }

    public float NormalizedPosition()
    {
        return transform.parent.CompareTag("Slot") ? ExtensionMethods.Remap((float)ParentIndex(), 0, (float)(transform.parent.parent.childCount - 1), 0, 1) : 0;
    }

    private void OnDestroy()
    {
        if (cardVisual != null)
            Destroy(cardVisual.gameObject);
    }
}
