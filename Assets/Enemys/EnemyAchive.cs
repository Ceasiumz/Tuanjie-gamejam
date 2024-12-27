using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAchive : MonoBehaviour
{
    // Start is called before the first frame aupdate
    public int infinityAddAttack;
    public int infinityAddHealth;
    public int infinityTurn;
    [SerializeField] public HorizontalCardHolder enemyHolder;
    [SerializeField] public HorizontalCardHolder playerHolder;
    List<GameObject> enemyList;
    [SerializeField] EnemyBase enemy;
    [SerializeField]public int enemyIndex;
    public GameObject E0dieDialogue;
    public GameObject E1dieDialogue;
    public GameObject E2dieDialogue;
    public GameObject E3dieDialogue;
    public GameObject E6dieDialogue;
    public GameObject E7dieDialogue;
    void Awake()
    {   
        DontDestroyOnLoad(this);
        enemyList = new List<GameObject>();
    }
    private void Start() {
        EnemyInit();
        TurnManager.Instance.PlayerTurn_Start.AddListener(EnemySuspendToEndCheck);
        AddListeners(enemy);
    }
    void AddListeners(EnemyBase enemy){// addlisteners to enemy right now
        TurnManager.Instance.EnemyTurn_Start.AddListener(enemy.OnTurnStart);
        TurnManager.Instance.EnemyTurn_Draw.AddListener(enemy.OnTurnDraw);
        TurnManager.Instance.EnemyTurn_Suspend.AddListener(enemy.OnTurnSuspend);
        TurnManager.Instance.EnemyTurn_End.AddListener(enemy.OnTurnEnd);
        TurnManager.Instance.PlayerTurn_Start.AddListener(enemy.OnPlayerStart);
        TurnManager.Instance.PlayerTurn_Draw.AddListener(enemy.OnPlayerDraw);
        TurnManager.Instance.PlayerTurn_Suspend.AddListener(enemy.OnPlayerSuspend);
        TurnManager.Instance.PlayerTurn_End.AddListener(enemy.OnPlayerEnd);
    }
    void RemoveListeners(EnemyBase enemy){// remove listeners to enemy right now
        TurnManager.Instance.EnemyTurn_Start.RemoveListener(enemy.OnTurnStart);
        TurnManager.Instance.EnemyTurn_Draw.RemoveListener(enemy.OnTurnDraw);
        TurnManager.Instance.EnemyTurn_Suspend.RemoveListener(enemy.OnTurnSuspend);
        TurnManager.Instance.EnemyTurn_End.RemoveListener(enemy.OnTurnEnd);
        TurnManager.Instance.PlayerTurn_Start.RemoveListener(enemy.OnPlayerStart);
        TurnManager.Instance.PlayerTurn_Draw.RemoveListener(enemy.OnPlayerDraw);
        TurnManager.Instance.PlayerTurn_Suspend.RemoveListener(enemy.OnPlayerSuspend);
        TurnManager.Instance.PlayerTurn_End.RemoveListener(enemy.OnPlayerEnd);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemySuspendToEndCheck(){
        
    }

    IEnumerator WaitingEnemySuspendToEndCheck(){
        yield return new WaitForSeconds(0.5f);
        if(GamePointBoard.Instance.isEnemySuspension){
            TurnManager.Instance.EnemyTurn_Start
        .RemoveListener(TurnManager.Instance.EnemyTurn_end);
        }
    }
    public void NextEnemy(){
        if (enemyIndex == 0)
        {
            E0dieDialogue.SetActive(true); 
        }
        if (enemyIndex == 1)
        {
            E1dieDialogue.SetActive(true);
        }
        if (enemyIndex == 2)
        {
            E2dieDialogue.SetActive(true);
        }
        if (enemyIndex == 3)
        {
            E3dieDialogue.SetActive(true);
        }
        if (enemyIndex == 4)
        {
            E6dieDialogue.SetActive(true);
        }
        if (enemyIndex == 5)
        {
            E7dieDialogue.SetActive(true);
        }
        RemoveListeners(enemy); 
        enemyIndex++;
        if(enemyIndex >= enemyList.Count){
            enemyIndex = 0;
            infinityTurn++;
            infinityAddAttack += 5* infinityTurn;
            infinityAddHealth += 10* infinityTurn;
        }
        SelectEnemy(enemyIndex);
        AddListeners(enemy);
        GamePointBoard.Instance.ResetSuspension();
        DynamicEventBus.Publish("EnemyChangeEvent");
    }
    public void SelectEnemy(int IndexOfEnemyToSelect){
        enemy.gameObject.SetActive(false);
        enemy = enemyList[IndexOfEnemyToSelect].GetComponent<EnemyBase>();
        enemy.gameObject.SetActive(true);
        GamePointBoard.Instance.enemyMaxHealth = enemy.health + infinityAddHealth;
        GamePointBoard.Instance.enemyAttack = enemy.attack + infinityAddAttack;
        GamePointBoard.Instance.enemyCurrentHealth = GamePointBoard.Instance.enemyMaxHealth;
    }
    public void EnemyInit(){
        infinityTurn = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            enemyList.Add(child.gameObject);
            // 在这里可以对获取到的子游戏对象进行各种操作
            if(i != enemyIndex)
            {
                child.gameObject.SetActive(false);
            }else{
                enemy = child.gameObject.GetComponent<EnemyBase>();
            }   
        }
        GamePointBoard.Instance.enemyMaxHealth = enemy.health + infinityAddHealth;
        GamePointBoard.Instance.enemyAttack = enemy.attack + infinityAddAttack;
        GamePointBoard.Instance.enemyCurrentHealth = GamePointBoard.Instance.enemyMaxHealth;
    }
}
