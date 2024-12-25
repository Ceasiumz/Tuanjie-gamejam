using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class SkillSelectButton : MonoBehaviour
{

    public BaseSkill skill;
    public Sprite skillImage;
    public string skillName;
    public string skillRemark;

    public void Init(BaseSkill skill)
    {
        // GetComponentInChildren<Image>().sprite = null;
        this.skill = skill;
        GetComponentInChildren<Text>().text = skill.skillName+":" +skill.remark;
    }

    public void AddSkill()
    {
        SkillPool.Instance.AddPlayerSkill(skill);
    }
}
