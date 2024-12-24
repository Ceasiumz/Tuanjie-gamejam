using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private GamePointBoard gamePointBoard;
    private Text healthText;
    public GameObject PlayerHead_healthy;
    public GameObject PlayerHead_dying;
    private Text enemyAttack;
    void Start()
    {
        PlayerHead_healthy.SetActive(true);
        PlayerHead_dying.SetActive(false    );
        gamePointBoard = FindObjectOfType<GamePointBoard>();
        if (gamePointBoard == null)
        {
            Debug.LogError("找不到GamePointBoard脚本所在的对象，请检查场景设置！");
            return;
        }


        healthText = GetComponent<Text>();
        if (healthText == null)
        {

            healthText = GetComponentInChildren<Text>();
        }

        UpdateHealthText();
    }

    void Update()
    {

        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (healthText != null && gamePointBoard != null)
        {
            //Debug.Log(gamePointBoard.currentHealth+"|||" + gamePointBoard.enemyAttack);
            healthText.text = gamePointBoard.currentHealth.ToString();
            if(gamePointBoard.currentHealth <= gamePointBoard.enemyAttack )
            {
                PlayerHead_dying.SetActive(true);
            }
            if (gamePointBoard.currentHealth > gamePointBoard.enemyAttack)
            {
                PlayerHead_dying.SetActive(false);
            }
        }
    }   
}