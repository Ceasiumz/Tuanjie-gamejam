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
            healthText.text = gamePointBoard.enemyCurrentHealth.ToString();

        }
    }
}
