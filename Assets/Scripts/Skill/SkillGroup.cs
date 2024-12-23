using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillGroup", menuName = "Data/SkillGroup/BaseGroup")]
public class SkillGroup : ScriptableObject
{
    public string skillGroupId;
    public string skillGroupName;

    public List<BaseSkill> skills;
}
