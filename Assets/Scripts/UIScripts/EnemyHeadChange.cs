using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeadChange : MonoBehaviour
{
    public Sprite enemyHead_1;
    public Sprite enemyHead_dying_1;
    public Sprite enemyHead_2;
    public Sprite enemyHead_dying_2;
    public Sprite enemyHead_3;
    public Sprite enemyHead_dying_3;
    public Sprite enemyHead_4;
    public Sprite enemyHead_dying_4;
    public Sprite enemyHead_5;
    public Sprite enemyHead_dying_5;
    public Sprite enemyHead_6;
    public Sprite enemyHead_dying_6;

    public EnemyAchive enemy;


    public void OnEnable()
    {
        DynamicEventBus.Subscribe("EnemyChangeEvent", ChangeHead);
        DynamicEventBus.Subscribe("AfterPlayerAttackEvent", ChangeDyingHead);
    }

    public void ChangeHead()
    {
        switch (enemy.enemyIndex)
        {
            case 0:
                GetComponent<Image>().sprite = enemyHead_1;
                break;
            case 1:
                GetComponent<Image>().sprite = enemyHead_2;
                break;
            case 2:
                GetComponent<Image>().sprite = enemyHead_3;
                break;
            case 3:
                GetComponent<Image>().sprite = enemyHead_4;
                break;
            case 4:
                GetComponent<Image>().sprite = enemyHead_5;
                break;
            case 5:
                GetComponent<Image>().sprite = enemyHead_6;
                break;
            default:
                GetComponent<Image>().sprite = enemyHead_1;
                break;
        }
    }

    public void ChangeDyingHead()
    {
        
        if (GamePointBoard.Instance.attack >= GamePointBoard.Instance.enemyCurrentHealth)
        {
            switch (enemy.enemyIndex)
            {
                case 0:
                    GetComponent<Image>().sprite = enemyHead_dying_1;
                    break;
                case 1:
                    GetComponent<Image>().sprite = enemyHead_dying_2;
                    break;
                case 2:
                    GetComponent<Image>().sprite = enemyHead_dying_3;
                    break;
                case 3:
                    GetComponent<Image>().sprite = enemyHead_dying_4;
                    break;
                case 4:
                    GetComponent<Image>().sprite = enemyHead_dying_5;
                    break;
                case 5:
                    GetComponent<Image>().sprite = enemyHead_dying_6;
                    break;
                default:
                    GetComponent<Image>().sprite = enemyHead_dying_1;
                    break;
            }
        }
        
    }
    
    public void OnDestroy()
    {
        DynamicEventBus.Unsubscribe("EnemyChangeEvent", ChangeHead);
        DynamicEventBus.Unsubscribe("AfterPlayerAttackEvent", ChangeDyingHead);
    }
}
