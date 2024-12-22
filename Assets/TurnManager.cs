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
        public bool PlayerTurn_Start;
        public bool PlayerTurn_Draw;
        public bool PlayerTurn_End;
        public bool EnemyTurn_Start;
        public bool EnemyTurn_Draw;
        public bool EnemyTurn_End;

        public Turn(bool playerTurnStart, bool playerTurnDraw, bool playerTurnEnd
        , bool enemyTurnStart, bool enemyTurnDraw, bool enemyTurnEnd)
        {
            PlayerTurn_Start = playerTurnStart;
            PlayerTurn_Draw = playerTurnDraw;
            PlayerTurn_End = playerTurnEnd;
            EnemyTurn_Start = enemyTurnStart;
            EnemyTurn_Draw = enemyTurnDraw;
            EnemyTurn_End = enemyTurnEnd;
        }
    }
    void Start()
    {
        PlayerTurn_start();
    }
    public void PlayerTurn_start()
    {
        turn = new Turn(false, false, false, false, false, false);
        if (turn.PlayerTurn_Start == false)
        {
            PlayerTurn_Start.Invoke();
            turn.PlayerTurn_Start = true;
        }
    }
    public void PlayerTurn_draw()
    {
        if (turn.PlayerTurn_Draw == false && turn.PlayerTurn_Start == true)
        {
            PlayerTurn_Draw.Invoke();
            turn.PlayerTurn_Draw = true;
        }
    }

    public void PlayerTurn_suspend()
    {
        if (turn.PlayerTurn_Draw == false && turn.PlayerTurn_Start == true)
        {
            PlayerTurn_Suspend.Invoke();
            turn.PlayerTurn_Draw = true;
        }
    }
    public void PlayerTurn_end()
    {
        if (turn.PlayerTurn_End == false && turn.PlayerTurn_Draw == true)
        {
            PlayerTurn_End.Invoke();
            turn.PlayerTurn_End = true;
            EnemyTurn_start();
        }
    }
    public void EnemyTurn_start()
    {
        if (turn.EnemyTurn_Start == false)
        {
            EnemyTurn_Start.Invoke();
            turn.EnemyTurn_Start = true;
        }
    }
    public void EnemyTurn_draw()
    {
        if (turn.EnemyTurn_Draw == false && turn.EnemyTurn_Start == true)
        {
            EnemyTurn_Draw.Invoke();
            turn.EnemyTurn_Draw = true;
        }
    }
    public void EnemyTurn_end()
    {
        if (turn.EnemyTurn_End == false && turn.EnemyTurn_Draw == true)
        {
            EnemyTurn_End.Invoke();
            turn.EnemyTurn_End = true;
            PlayerTurn_start();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
