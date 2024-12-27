using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

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
    public AudioSource attack;
    public AudioSource injured;
    public AudioSource noWinner;
    Text text;
    public Text tipText;
    public Text ScoreText;
    public Text Kills;
    public float fadeDuration = 2f; // 渐变持续时间
    public string nextSceneName; // 要切换到的场景名称
    public GameObject fadeIMage;
    Image fadeImage;
    private void Awake()
    {
        eA = FindObjectOfType<EnemyAchive>();
    }
    void Start()
    {
        fadeImage = fadeIMage.GetComponent<Image>();
        fadeIMage.SetActive(false);
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
        text.text =  GamePointBoard.Instance.playerCardPoints+"/21";
    }


    //爆牌结算
    public void BurstSettlement(bool isEnemy)
    {
        //敌人爆牌结算
        if (isEnemy)
        {
            PlayerAttack();
            ScoreAdd();
            tipText.text = "对方爆牌啦！";
        }
        else
        {
            //玩家爆牌结算
            EnemyAttack();
            tipText.text = "我方爆牌了！";
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
            tipText.text = "对方胜利了！";
        }
        else//平局
        {
            tipText.text = "打平了！";
            noWinner.Play();
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
            tipText.text = "敌方到达了21点！";
        }
        else//玩家获胜
        {
            PlayerAttack();
            ScoreAdd();
            tipText.text = "我方到达了21点！";
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
            tipText.text = "我方战败";
        }
        //技能判定后重新判断血量
        if (GamePointBoard.Instance.currentHealth <= 0)
        {
            DynamicEventBus.Publish("RoundEndEvent");
            Debug.Log("玩家死亡");
            fadeIMage.SetActive(true);
            GamePointBoard.Instance.resetHealth();
            SkillPool.Instance.RoundStart();
            StartCoroutine(FadeOut());
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
            Debug.Log("敌人死亡A115判断");
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
    public static void Restart()// latecheck if the ally points are over lim
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //玩家小局获胜对敌人造成伤害
    public void PlayerAttack()
    {
        attack.Play();
        DynamicEventBus.Publish("BeforePlayerAttackEvent");
        Debug.Log("本次攻击力"+GamePointBoard.Instance.attack);
        Debug.Log("本次攻击力加成"+GamePointBoard.Instance.attackAddition);
        Debug.Log("本次攻击力加成倍率"+GamePointBoard.Instance.attackMultiple);
        Debug.Log("本次造成伤害"+Mathf.RoundToInt((GamePointBoard.Instance.attack+GamePointBoard.Instance.attackAddition)*GamePointBoard.Instance.attackMultiple));
        if (GamePointBoard.Instance.attackMultiple>=2)
        {
            tipText.text = "由于宝牌，我方造成了" + Mathf.RoundToInt((GamePointBoard.Instance.attack + GamePointBoard.Instance.attackAddition) * GamePointBoard.Instance.attackMultiple)+"点伤害！";
        }
        else
        {
            tipText.text = "我方造成了" + Mathf.RoundToInt((GamePointBoard.Instance.attack + GamePointBoard.Instance.attackAddition) * GamePointBoard.Instance.attackMultiple) + "点伤害！";
        }
        GamePointBoard.Instance.enemyCurrentHealth -= Mathf.RoundToInt((GamePointBoard.Instance.attack+GamePointBoard.Instance.attackAddition)*GamePointBoard.Instance.attackMultiple); 
        DynamicEventBus.Publish("AfterPlayerAttackEvent");
        GamePointBoard.Instance.ResetDamageMultipl();
    }
    //玩家小局失败受到伤害
    public void EnemyAttack()
    {
        injured.Play();
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
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        // 渐变完成后切换场景
        SceneManager.LoadScene(nextSceneName);
    }
}
