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
    //攻击附加值
    public int attackAddition=0;
    //受伤减免值
    public int injuryReduction=0;
    //攻击伤害倍率
    public float attackMultiple=1f;
    //受伤伤害倍率
    public float injuryMultiple=1f;
    //玩家是否停牌
    public bool isPlayerSuspension;
    public  int playerCardPoints=0;
    //每轮结束后可以获得的技能数
    public int skillChouseNum = 1;
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
    private void Start() {
        TurnManager.Instance.EnemyTurn_Suspend.AddListener(RecordSuspensionE);
        TurnManager.Instance.PlayerTurn_Suspend.AddListener(RecordSuspensionP);

    }
    //在抽卡后更新卡牌点数
    public void UpdateCardPoints(bool isEnemy,List<Card> cards)
    {
        int tempoint = 0;
        foreach (var card in cards)
        {
            //点数结算
            tempoint+=card.points;
            
        }
        //计算超过21点后检查卡牌是否有A 如果有把卡牌改成1
        if (tempoint > 21)
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
            playerCardPoints = tempoint;
            //玩家卡牌点数结算前
            DynamicEventBus.Publish("BeforePlayerCardPointSettle");
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
    public void RecordSuspensionE()
    {
        bool isEnemy = true;
        //停牌后应禁用抽牌 停牌 和加注
        if(!isEnemy)//玩家停牌
        {
            isPlayerSuspension = true;
        }
        else//敌人停牌
        {
            isEnemySuspension = true;
        }
        //双方都停牌时 触发结算比较牌面大小
        if (isPlayerSuspension == true && isEnemySuspension == true)
        {
            AllyPoint.Instance.NormalSettlement();
            isPlayerSuspension = false;
            isEnemySuspension = false;
        }
    }
    public void RecordSuspensionP()
    {
        bool isEnemy = false;
        //停牌后应禁用抽牌 停牌 和加注
        if(!isEnemy)//玩家停牌
        {
            isPlayerSuspension = true;
        }
        else//敌人停牌
        {
            isEnemySuspension = true;
        }
        //双方都停牌时 触发结算比较牌面大小
        if (isPlayerSuspension == true && isEnemySuspension == true)
        {
            AllyPoint.Instance.NormalSettlement();
            isPlayerSuspension = false;
            isEnemySuspension = false;
        }
    }
    //清空卡牌点数计算
    public void ClearCardPoints()
    {
        playerCardPoints = 0;
        enemyCardPoints = 0;
    }

    public void ResetSuspension()
    {
        isPlayerSuspension = false;
        isEnemySuspension = false;
    }
    //重置伤害增值和倍率
    public void ResetDamageMultipl()
    {
        attackAddition = 0;
        attackMultiple = 1f;
        injuryReduction = 0;
        injuryMultiple = 1f;
    }
    
    
    //特殊 技能A107技能 卡牌存放点
    private List<Card> skillHiddenCard=new List<Card>();

    public List<Card> GetHiddenCard()
    {
        return skillHiddenCard;
    }

    public void HideCard(Card card)
    {
        skillHiddenCard.Add(card);
        AllyPoint.Instance.holder.cards.Remove(card);
    }

    public void destroyEvidence()
    {
        skillHiddenCard.Clear();
    }
    
}
