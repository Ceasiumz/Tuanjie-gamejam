using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static TurnManager _instance;
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
    public UnityEvent EnemyTurn_End;
    [SerializeField] public Turn turn;
    [System.Serializable] public struct Turn
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
    }
    public void PlayerTurn_start()
    {
        turn = new Turn(false, false, false, false, false, false);
        if (turn.playerTurn_Start == false)
        {
            turn.playerTurn_Start = true;
            PlayerTurn_Start.Invoke();
        }
    }
    public void PlayerTurn_draw()
    {
        if (turn.playerTurn_Draw == false && turn.playerTurn_Start == true)
        {
            turn.playerTurn_Draw = true;
            PlayerTurn_Draw.Invoke();
        }
    }

    public void PlayerTurn_suspend()
    {
        if (turn.playerTurn_Draw == false && turn.playerTurn_Start == true)
        {
            turn.playerTurn_Draw = true;
            PlayerTurn_Suspend.Invoke();
        }
    }
    public void PlayerTurn_end()
    {
        if (turn.playerTurn_End == false && turn.playerTurn_Draw == true)
        {
            turn.playerTurn_End = true;
            PlayerTurn_End.Invoke();
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
        }
    }
    public void EnemyTurn_end()
    {
        if (turn.enemyTurn_End == false && turn.enemyTurn_Draw == true)
        {
            turn.enemyTurn_End = true;
            EnemyTurn_End.Invoke();
            PlayerTurn_start();
        }
    }

    
    // Update is called once per frame
    void Update()
    {

    }
}
