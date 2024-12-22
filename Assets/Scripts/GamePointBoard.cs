using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePointBoard : MonoBehaviour
{
    [Header("Player")]
    //玩家相关属性
    public int maxHealth;
    public int currentHealth;
    public int attack;
    //玩家是否停牌
    public bool isPlayerSuspension;
    public  int cardPoints=0;
    [Header("Enemy")]
    //敌人相关属性
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int enemyAttack;
    //敌人是否停牌
    public bool isEnemySuspension;
    public int enemyCardPoints;
    
    private static GamePointBoard _instance;
    public static GamePointBoard Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GamePointBoard>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GamePointBoard");
                    _instance = obj.AddComponent<GamePointBoard>();
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
    //重新生成下一个敌人 方法 
    
    //在抽卡后更新卡牌点数
    public  void UpdatePlayerCardPoints(bool isEnemy,List<Card> cards)
    {
        int tempoint = 0;
        foreach (var card in cards)
        {
            //点数结算
            tempoint+=card.points;
            
        }
        //计算超过21点后检查卡牌是否有A 如果有把卡牌改成1
        if (tempoint>21)
        {
            int tempoint2 = 0;
            foreach (var card in cards)
            {
                if (card.name == "A")
                {
                    tempoint2 += 1;
                }
                else
                {
                    tempoint2 += card.points;
                }
            }
            tempoint=tempoint2;
        }
        
        if(!isEnemy)
        {
            cardPoints = tempoint;
        }
        else
        {
            enemyCardPoints = tempoint;
        }
        
        //21点直接胜利
        if (tempoint == 21)
        {
            AllyPoint.Instance.WinSettlement(isEnemy);
        }
        //爆牌结算
        if (tempoint > 21)
        {
            AllyPoint.Instance.BurstSettlement(isEnemy);
        }
    }
    
    //记录停牌操作
    public void RecordSuspension(bool isEnemy)
    {
        //停牌后应禁用抽牌 停牌 和加注
        if(!isEnemy)//玩家停牌
        {
            isPlayerSuspension = true;
            TurnManager.Instance.PlayerTurn_end();
        }
        else//敌人停牌
        {
            isEnemySuspension = true;
            TurnManager.Instance.EnemyTurn_end();
        }
        //双方都停牌时 触发结算比较牌面大小
        if (isPlayerSuspension == true && isEnemySuspension == true)
        {
            AllyPoint.Instance.NormalSettlement();
        }
    }
    //清空卡牌点数计算
    public void ClearCardPoints()
    {
        cardPoints = 0;
        enemyCardPoints = 0;
    }

    public void ResetSuspension()
    {
        isPlayerSuspension = false;
        isEnemySuspension = false;
    }
    
}
