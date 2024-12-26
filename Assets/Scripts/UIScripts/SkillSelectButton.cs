using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SkillSelectButton : MonoBehaviour
{

    public BaseSkill skill;

    public void Init(BaseSkill skill)
    {

        this.skill = skill;
        GetComponentInChildren<Image>().sprite = skill.skillImage;
        GetComponentInChildren<Text>().text = skill.skillName+":" +skill.remark;
    }

    public void AddSkill()
    {
        SkillPool.Instance.AddPlayerSkill(skill);
        Transform.Destroy(gameObject);
    }
}
