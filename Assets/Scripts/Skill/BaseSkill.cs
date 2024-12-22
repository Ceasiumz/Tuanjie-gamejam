using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[CreateAssetMenu(fileName = "Skill", menuName = "Data/Skill/BaseSkill")]
public  class BaseSkill : ScriptableObject
{
    public string skillName;
    //是否一次性
    public bool isOneTime;
    //是否为主动技能
    public bool isActive;
    public List<BaseEffect> skillEffect;
}