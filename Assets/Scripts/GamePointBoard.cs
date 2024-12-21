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
            tempoint+=card.points;
        }
        if(!isEnemy)
        {
            cardPoints = tempoint;
        }
        else
        {
            enemyCardPoints = tempoint;
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
        if(!isEnemy)
        {
            isPlayerSuspension = true;
        }
        else
        {
            isEnemySuspension = true;
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
    
}
