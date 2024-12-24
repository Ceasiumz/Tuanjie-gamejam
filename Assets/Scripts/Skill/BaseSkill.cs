using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[CreateAssetMenu(fileName = "Skill", menuName = "Data/Skill/BaseSkill")]
public  class BaseSkill : ScriptableObject
{
    public string skillID;
    public string skillName;
    
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

    public bool canUse;

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
            if (skillActivePhase == SkillActivePhase.BeforeDrawCard)
            {
                TurnManager.Instance.PlayerTurn_Start.AddListener(canUseSwitch);
            }
        }

    }
    public void canUseSwitch()
    {
        canUse = !canUse;
    }
}