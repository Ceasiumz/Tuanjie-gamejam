using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAttack : MonoBehaviour
{
    private GamePointBoard gamePointBoard;
    private Text attackText;
    void Start()
    {

        gamePointBoard = FindObjectOfType<GamePointBoard>();
        if (gamePointBoard == null)
        {
            Debug.LogError("�Ҳ���GamePointBoard�ű����ڵĶ������鳡�����ã�");
            return;
        }

        attackText = GetComponent<Text>();
        if (attackText == null)
        {
            attackText = GetComponentInChildren<Text>();
        }

        UpdateAttackText();
    }

    void Update()
    {

        UpdateAttackText();
    }

    void UpdateAttackText()
    {
        if (attackText != null && gamePointBoard != null)
            attackText.text = gamePointBoard.enemyAttack.ToString();
    }
}
