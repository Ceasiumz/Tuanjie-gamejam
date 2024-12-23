using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "Data/SkillEffect/A110Effect")]
public class A110Effect : BaseEffect
{
    public override void subscribeEvent()
    {

    }

    public override void unsubscribeEvent()
    {

    }

    public override void Execute()
    {
        List<Card> playerCard = AllyPoint.Instance.holder.cards;
        List<Card> enemyCard = AllyPoint.Instance.ememyHolder.cards;
        
        //获取玩家手牌中点数最小的牌
        Card minCard = playerCard[0];
        for (int i = 1; i < playerCard.Count; i++)
        {
            if (playerCard[i].points < minCard.points)
            {
                minCard = playerCard[i];
            }
        }
        //将卡牌点数还原回原始点数
        switch (minCard.name)
        {
            case "A":
                minCard.points = 11;
                break;
            case "J":
                minCard.points = 10;
                break;
            case "Q":
                minCard.points = 10;
                break;
            case "K":
                minCard.points = 10;
                break;
            default:
                minCard.points = int.Parse(minCard.name);
                break;
        }
        enemyCard.Add(minCard);
        if (minCard.name == "A")
        {
            //计算敌人卡牌点数和
            int enemyCardPoints = 0;
            foreach (Card card in enemyCard)
            {
                enemyCardPoints += card.points;
            }

            if (enemyCardPoints > 21)
            {
                minCard.points = 1;
            }
        }
        playerCard.Remove(minCard);
        GamePointBoard.Instance.UpdatePlayerCardPoints(false, playerCard);
        GamePointBoard.Instance.UpdatePlayerCardPoints(true, enemyCard);

    }

    public override void ImmediateTrigger()
    {

    }
}
