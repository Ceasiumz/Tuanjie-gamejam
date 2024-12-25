using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[CreateAssetMenu(fileName = "Skill", menuName = "Data/Skill/BaseSkill")]
public  class BaseSkill : ScriptableObject
{
    public string skillID;
    public string skillName;
    public Sprite skillImage;
    //是否一次性
    public bool isOneTime;
    //是否为主动技能
    public SkillType skillType;

    //技能释放阶段
    public SkillActivePhase skillActivePhase;
    //是否可重复获得
    public bool isRepeatable;
    //技能使用后是否回到技能池
    public bool isUsedReturn;

    public bool canUse=false;

    //是否禁止玩家主动获得
    public bool isHidden;
    //备注
    [TextArea(3, 10)]
    public string remark;
    public List<BaseEffect> skillEffect;

    public void subscribeTurnEvent()
    {
        if (skillType == SkillType.Active)
        {
            //抽卡前释放技能 
            if (skillActivePhase == SkillActivePhase.BeforeDrawCard)
            {
                //在回合开始阶段 切换技能可释放
                TurnManager.Instance.PlayerTurn_Start.AddListener(canUseSwitchToTrue);
                TurnManager.Instance.PlayerTurn_Draw.AddListener(canUseSwitchToFalse);
                TurnManager.Instance.PlayerTurn_Suspend.AddListener(canUseSwitchToFalse);
            }

            if (skillActivePhase == SkillActivePhase.AfterDrawCard)
            {
                TurnManager.Instance.PlayerTurn_Draw.AddListener(canUseSwitchToTrue);
                TurnManager.Instance.PlayerTurn_Suspend.AddListener(canUseSwitchToTrue);
                TurnManager.Instance.PlayerTurn_End.AddListener(canUseSwitchToFalse);
            }
        }

    }
    public void canUseSwitchToTrue()
    {
        canUse = true;
    }

    public void canUseSwitchToFalse()
    {
        canUse = false;
    }
    public void unsubscribeTurnEvent()
    {
        if (skillType == SkillType.Active)
        {
            //抽卡前释放技能 
            if (skillActivePhase == SkillActivePhase.BeforeDrawCard)
            {
                TurnManager.Instance.PlayerTurn_Start.RemoveListener(canUseSwitchToTrue);
                TurnManager.Instance.PlayerTurn_Draw.RemoveListener(canUseSwitchToFalse);
                TurnManager.Instance.PlayerTurn_Suspend.RemoveListener(canUseSwitchToFalse);
            }

            if (skillActivePhase == SkillActivePhase.AfterDrawCard)
            {
                TurnManager.Instance.PlayerTurn_Draw.RemoveListener(canUseSwitchToTrue);
                TurnManager.Instance.PlayerTurn_Suspend.RemoveListener(canUseSwitchToTrue);
                TurnManager.Instance.PlayerTurn_End.RemoveListener(canUseSwitchToFalse);
            }
        }
    }
}