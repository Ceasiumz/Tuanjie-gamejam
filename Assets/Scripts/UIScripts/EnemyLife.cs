using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyLife : MonoBehaviour
{
    private GamePointBoard gamePointBoard;
    private Text healthText;

    void Start()
    {

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
            healthText.text = gamePointBoard.enemyCurrentHealth.ToString();

        }
    }
}
