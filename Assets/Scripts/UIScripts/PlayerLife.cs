using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private GamePointBoard gamePointBoard;
    private Text healthText;
    public GameObject PlayerHead_healthy;
    public GameObject PlayerHead_dying;
    private Text enemyAttack;
    public GameObject dyingUI;
    void Start()
    {
        PlayerHead_healthy.SetActive(true);
        PlayerHead_dying.SetActive(false);
        dyingUI.SetActive(false);
        gamePointBoard = FindObjectOfType<GamePointBoard>();
        if (gamePointBoard == null)
        {
            Debug.LogError("�Ҳ���GamePointBoard�ű����ڵĶ������鳡�����ã�");
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
                dyingUI.SetActive(true);
            }
            if (gamePointBoard.currentHealth > gamePointBoard.enemyAttack)
            {
                PlayerHead_dying.SetActive(false);
            }
        }
    }   
}