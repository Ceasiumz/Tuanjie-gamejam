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
    public Text ScoreText;
    public Text Kills;
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
            ScoreAdd();
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
            ScoreAdd();
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
            ScoreAdd();
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
            Debug.Log("玩家死亡");
            SkillPool.Instance.RoundStart();
            GamePointBoard.Instance.resetHealth();
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
            KillsAdd();
            DynamicEventBus.Publish("RoundEndEvent");
            
            //补丁代码
            if (GamePointBoard.Instance.skillChouseNum == 2)
            {
                GamePointBoard.Instance.skillChouseNum = 2;
            }
            else
            {
                GamePointBoard.Instance.skillChouseNum = 1;
            }

            eA.NextEnemy();
            DrawOutEvent.Invoke();
            //洗牌 拼写错误
            holder.DiscoverCardDeck();
            SkillPool.Instance.OpenSkillSelectDialog();
        }
    }
    public void Restart()// latecheck if the ally points are over lim
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //玩家小局获胜对敌人造成伤害
    public void PlayerAttack()
    {
        DynamicEventBus.Publish("BeforePlayerAttackEvent");
        Debug.Log("本次攻击力"+GamePointBoard.Instance.attack);
        Debug.Log("本次攻击力加成"+GamePointBoard.Instance.attackAddition);
        Debug.Log("本次攻击力加成倍率"+GamePointBoard.Instance.attackMultiple);
        Debug.Log("本次造成伤害"+Mathf.RoundToInt((GamePointBoard.Instance.attack+GamePointBoard.Instance.attackAddition)*GamePointBoard.Instance.attackMultiple));
        GamePointBoard.Instance.enemyCurrentHealth -= Mathf.RoundToInt((GamePointBoard.Instance.attack+GamePointBoard.Instance.attackAddition)*GamePointBoard.Instance.attackMultiple); 
        DynamicEventBus.Publish("AfterPlayerAttackEvent");
        GamePointBoard.Instance.ResetDamageMultipl();
    }
    //玩家小局失败受到伤害
    public void EnemyAttack()
    {
        DynamicEventBus.Publish("BeforeEnemyAttackEvent");
        Debug.Log("本次伤害减免"+GamePointBoard.Instance.injuryReduction);
        GamePointBoard.Instance.currentHealth -= Mathf.Max(1,Mathf.RoundToInt((GamePointBoard.Instance.enemyAttack-GamePointBoard.Instance.injuryReduction)*GamePointBoard.Instance.injuryMultiple))  ;
        DynamicEventBus.Publish("AfterEnemyAttackEvent");
        GamePointBoard.Instance.ResetDamageMultipl();
    }

    public void AddPoints(Card card)
    {

    }
    void ScoreAdd()
    {
        int currentScore = string.IsNullOrEmpty(ScoreText.text) ? 0 : int.Parse(ScoreText.text);
        // 进行分数增加运算
        currentScore += 10;
        // 将更新后的数字转换回字符串并赋值给Text组件的text属性，用于显示
        ScoreText.text = currentScore.ToString();
    }
    void KillsAdd()
    {
        int currentKills = string.IsNullOrEmpty(Kills.text) ? 0 : int.Parse(Kills.text);
        // 进行杀敌数增加运算
        currentKills += 1;
        // 将更新后的数字转换回字符串赋值给Text组件的text属性以显示
        Kills.text = currentKills.ToString();
    }
}
