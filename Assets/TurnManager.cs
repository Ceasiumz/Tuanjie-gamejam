using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int turnCount;
    private static TurnManager _instance;
    public GameObject PlayersuspendButton;
    public GameObject EnemysuspendButton;
    public GameObject PlayerEndButton;
    public GameObject PlayerDrawButton;
    public GameObject veil;
    public HorizontalCardHolder playerHolder;
    public bool StartDrawing = false;
    bool drawLock = false;
    public Text tipText;
    public static TurnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TurnManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("TurnManager");
                    _instance = obj.AddComponent<TurnManager>();
                }
            }
            return _instance;
        }
    }

    public UnityEvent PlayerTurn_Start;
    public UnityEvent PlayerTurn_Draw;
    public UnityEvent PlayerTurn_Suspend;
    public UnityEvent PlayerTurn_End;
    public UnityEvent EnemyTurn_Start;
    public UnityEvent EnemyTurn_Draw;
    public UnityEvent EnemyTurn_Suspend;
    public UnityEvent EnemyTurn_End;
    [SerializeField] public Turn turn;
    [System.Serializable]
    public struct Turn
    {
        public bool playerTurn_Start;
        public bool playerTurn_Draw;
        public bool playerTurn_End;
        public bool enemyTurn_Start;
        public bool enemyTurn_Draw;
        public bool enemyTurn_End;

        public Turn(bool playerTurnStart, bool playerTurnDraw, bool playerTurnEnd
        , bool enemyTurnStart, bool enemyTurnDraw, bool enemyTurnEnd)
        {
            playerTurn_Start = playerTurnStart;
            playerTurn_Draw = playerTurnDraw;
            playerTurn_End = playerTurnEnd;
            enemyTurn_Start = enemyTurnStart;
            enemyTurn_Draw = enemyTurnDraw;
            enemyTurn_End = enemyTurnEnd;
        }
    }
    void Start()
    {
        PlayerTurn_start();
        Doveil();
    }
    public void Doveil(){
        StartCoroutine(veilWait());
    }
    IEnumerator veilWait(){
        veil.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        veil.SetActive(false);
    }
    public void PlayerTurn_start()
    {
        turn = new Turn(false, false, false, false, false, false);
        turnCount++;
        // Debug.Log("new turn");
        if (turn.playerTurn_Start == false)
        {
            PlayerEndButton.GetComponent<Image>().color = Color.white;
            if (!StartDrawing)
            {
                PlayerDrawButton.SetActive(true);
            }
            PlayersuspendButton.SetActive(true);
            turn.playerTurn_Start = true;
            PlayerTurn_Start.Invoke();
        }
        StartCoroutine(SuspendWait());
    }

    IEnumerator SuspendWait()
    {
        
        yield return new WaitForSeconds(0.4f);
        if (GamePointBoard.Instance.isPlayerSuspension)
        {
            turn.playerTurn_Draw = true;
            PlayerDrawButton.SetActive(false);
            PlayersuspendButton.SetActive(false);
        }
    }
    public void PlayerTurn_draw()
    {
        if(!drawLock)
            StartCoroutine(DrawWaiting());
        
    }
    IEnumerator DrawWaiting(){
        drawLock = true;
        if (playerHolder.cards.Count == 0)
        {
            turn.playerTurn_Draw = false;
        }
        if (turn.playerTurn_Draw == false && turn.playerTurn_Start == true && !StartDrawing)
        {
            turn.playerTurn_Draw = true;
            PlayerDrawButton.SetActive(false);
            PlayersuspendButton.SetActive(false);
            PlayerTurn_Draw.Invoke();
        }
        yield return new WaitForSeconds(0.7f);
        drawLock = false;
    }

    public void PlayerTurn_suspend()
    {
        if (turn.playerTurn_Draw == false && turn.playerTurn_Start == true)
        {
            turn.playerTurn_Draw = true;
            PlayerDrawButton.SetActive(false);
            PlayersuspendButton.SetActive(false);
            PlayerTurn_Suspend.Invoke();
        }
    }
    public void PlayerTurn_end()
    {
        if (turn.playerTurn_End == false && turn.playerTurn_Draw == true)
        {
            turn.playerTurn_End = true;
            PlayerTurn_End.Invoke();
            PlayerEndButton.GetComponent<Image>().color = new Color(181f, 143f, 143f);
            EnemyTurn_start();
        }
    }
    public void EnemyTurn_start()
    {
        if (turn.enemyTurn_Start == false)
        {
            turn.enemyTurn_Start = true;
            EnemyTurn_Start.Invoke();
        }
        if (GamePointBoard.Instance.isEnemySuspension)
        {
            turn.enemyTurn_Draw = true;
        }
    }
    public void EnemyTurn_draw()
    {
        //if(GamePointBoard.Instance.isEnemySuspension){
        //   turn.enemyTurn_Draw = true;
        //}
        //Debug.Log("EnemyTurn_draw"+turn.enemyTurn_Draw+"enemyTurn_Start"+turn.enemyTurn_Start);
        if (turn.enemyTurn_Draw == false && turn.enemyTurn_Start == true)
        {
            turn.enemyTurn_Draw = true;
            EnemyTurn_Draw.Invoke();
            tipText.text = "对方抽牌";
        }
        
    }
    public void EnemyTurn_suspend()
    {
        turn.enemyTurn_Draw = true;
        tipText.text = "对方停牌";
        EnemyTurn_Suspend.Invoke(); 
    }
    public void EnemyTurn_end()
    {
        if (turn.enemyTurn_End == false && turn.enemyTurn_Draw == true)
        {
            tipText.text = "对方结束回合";
            turn.enemyTurn_End = true;
            EnemyTurn_End.Invoke();
            PlayerTurn_start();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(turn.playerTurn_Draw){
            PlayerDrawButton.SetActive(false);
        }else{
            PlayerDrawButton.SetActive(true);
        }

        if(turn.enemyTurn_Draw){
            
        }
    }
}
