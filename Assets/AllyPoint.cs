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

    public static AllyPoint Instance;
    // public CharacterBase Player;
    // public CharacterBase Enemy;
    public int lim = 21;
    public HorizontalCardHolder holder;
    public UnityEvent DrawOutEvent;
    Text text;
    private bool isDead = false;

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
        int tempAllyPoints = 0;
        foreach (Card card in holder.cards)
        {
            tempAllyPoints += card.points;
        }
        if (tempAllyPoints != allyPoints)
        {
            allyPoints = tempAllyPoints;
            // DrawOutTest();
        }
        text.text = "Ally Points: " + allyPoints;
    }

    public void DrawOutTest()// check if the ally points are over lim
    {
        //角色未死亡
        if (isDead == false)
        {
            //TODO:清除场景中卡牌
            //角色局数获胜
            if (allyPoints == lim)
            {
                // Enemy.currentHealth -= Player.attack;
                DrawOutEvent.Invoke();
            }
            //角色局数失败
            else if (allyPoints > lim)
            {
                //受伤 刷新牌桌上卡牌
                // Player.currentHealth -= Enemy.attack;
                DrawOutEvent.Invoke();
            }
            
        }
        
        // if(Player.currentHealth<=0)
        // {
        //     isDead = true;
        // }
        
        //角色死亡 重置场景
        if (isDead == true)
        {
            DrawOutEvent.Invoke();
            holder.DrawButton.SetActive(false);
            Invoke("Restart", 1f);
        }
    }
    
    //爆牌结算
    public void BurstSettlement(bool isEnemy)
    {
        //敌人爆牌结算
        if (isEnemy)
        {
            //TODO:清除场景中卡牌
                //受伤 刷新牌桌上卡牌
                GamePointBoard.Instance.enemyCurrentHealth -= GamePointBoard.Instance.attack;

        }
        else
        {
            //玩家爆牌结算
            GamePointBoard.Instance.currentHealth -= GamePointBoard.Instance.enemyAttack;
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
        if (GamePointBoard.Instance.cardPoints > GamePointBoard.Instance.enemyCardPoints)
        {
            GamePointBoard.Instance.enemyCurrentHealth -= GamePointBoard.Instance.attack;
        }else if(GamePointBoard.Instance.cardPoints < GamePointBoard.Instance.enemyCardPoints)//玩家失败
        {
            GamePointBoard.Instance.currentHealth -= GamePointBoard.Instance.enemyAttack;
        }else//平局
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
            GamePointBoard.Instance.currentHealth -= GamePointBoard.Instance.enemyAttack;
        }
        else//玩家获胜
        {
            GamePointBoard.Instance.enemyCurrentHealth -= GamePointBoard.Instance.attack;
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
        if (GamePointBoard.Instance.currentHealth <= 0)
        {
            Restart();
        }
    }
    //敌人死亡判断
    public void EnemyDeadCheck()
    {
        if (GamePointBoard.Instance.enemyCurrentHealth <= 0)
        {
            // TODO:对手死亡处理 暂定处理重新加满生命值
            GamePointBoard.Instance.enemyCurrentHealth = GamePointBoard.Instance.enemyMaxHealth;
            DrawOutEvent.Invoke();
            holder.DiscoverCardDeck();
        }
    }
    public void Restart()// latecheck if the ally points are over lim
    {
        if (allyPoints > lim)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else
        {
            holder.DrawButton.SetActive(true);
        }
    }


    public void AddPoints(Card card)
    {

    }
}
