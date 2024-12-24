using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class AllyPoint : MonoBehaviour
{
    //TODO:可考虑把此点数计分修改为结算用
    // Start is called before the first frame update

    public int allyPoints;
    protected EnemyAchive eA;

    public static AllyPoint Instance;
    // public CharacterBase Player;
    // public CharacterBase Enemy;
    public int lim = 21;
    public HorizontalCardHolder holder;
    public HorizontalCardHolder ememyHolder;
    public UnityEvent DrawOutEvent;
    Text text;

    private void Awake()
    {
        eA = FindObjectOfType<EnemyAchive>();
    }
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        text = GetComponent<Text>();
        holder.DrawEvent.AddListener(AddPoints);
    }

    // Update is called once per frame
    void Update()// revise ally points
    {
        text.text = "Ally Points: " + GamePointBoard.Instance.playerCardPoints;
    }


    //爆牌结算
    public void BurstSettlement(bool isEnemy)
    {
        //敌人爆牌结算
        if (isEnemy)
        {
            PlayerAttack();
        }
        else
        {
            //玩家爆牌结算
            EnemyAttack();
        }
        DrawOutEvent.Invoke();
        GamePointBoard.Instance.ClearCardPoints();
        GamePointBoard.Instance.ResetSuspension();
        //死亡判断
        DeadCheck();
        EnemyDeadCheck();
    }
    //正常结算
    public void NormalSettlement()
    {
        //玩家获胜
        if (GamePointBoard.Instance.playerCardPoints > GamePointBoard.Instance.enemyCardPoints)
        {
            PlayerAttack();
        }
        else if (GamePointBoard.Instance.playerCardPoints < GamePointBoard.Instance.enemyCardPoints)//玩家失败
        {
            EnemyAttack();
        }
        else//平局
        {

        }
        DrawOutEvent.Invoke();
        GamePointBoard.Instance.ClearCardPoints();
        GamePointBoard.Instance.ResetSuspension();
        //死亡判断
        DeadCheck();
        EnemyDeadCheck();
    }

    //21点胜利结算
    public void WinSettlement(bool isEnemy)
    {
        //敌人获胜
        if (isEnemy)
        {
            EnemyAttack();
        }
        else//玩家获胜
        {
            PlayerAttack();
        }
        DrawOutEvent.Invoke();
        GamePointBoard.Instance.ClearCardPoints();
        //死亡判断
        DeadCheck();
        EnemyDeadCheck();
    }

    //玩家死亡判断 玩家死亡重新加载场景
    public void DeadCheck()
    {
        //玩家死亡时技能判定
        if (GamePointBoard.Instance.currentHealth <= 0)
        {
            DynamicEventBus.Publish("PlayerDeadEvent");
        }
        //技能判定后重新判断血量
        if (GamePointBoard.Instance.currentHealth <= 0)
        {
            DynamicEventBus.Publish("RoundEndEvent");
            Restart();
        }
    }
    //敌人死亡判断
    public void EnemyDeadCheck()
    {
        if (GamePointBoard.Instance.enemyCurrentHealth <= 0)
        {
            // 敌人死亡时选择下一个敌人
            Debug.Log("Enemy Dead");
            DynamicEventBus.Publish("RoundEndEvent");
            GamePointBoard.Instance.skillChouseNum = 1;
            eA.NextEnemy();
            DrawOutEvent.Invoke();
            holder.DiscoverCardDeck();
        }
    }
    public void Restart()// latecheck if the ally points are over lim
    {
        if (allyPoints > lim)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            holder.DrawButton.SetActive(true);
        }
    }

    //玩家小局获胜对敌人造成伤害
    public void PlayerAttack()
    {
        DynamicEventBus.Publish("BeforePlayerAttackEvent");
        GamePointBoard.Instance.enemyCurrentHealth -= Mathf.RoundToInt((GamePointBoard.Instance.attack+GamePointBoard.Instance.attackAddition)*GamePointBoard.Instance.attackMultiple); 
        DynamicEventBus.Publish("AfterPlayerAttackEvent");
        GamePointBoard.Instance.ResetDamageMultipl();
    }
    //玩家小局失败受到伤害
    public void EnemyAttack()
    {
        DynamicEventBus.Publish("BeforeEnemyAttackEvent");
        GamePointBoard.Instance.currentHealth -= Mathf.Max(1,Mathf.RoundToInt((GamePointBoard.Instance.enemyAttack-GamePointBoard.Instance.injuryReduction)*GamePointBoard.Instance.injuryMultiple))  ;
        DynamicEventBus.Publish("AfterEnemyAttackEvent");
        GamePointBoard.Instance.ResetDamageMultipl();
    }

    public void AddPoints(Card card)
    {

    }
}
