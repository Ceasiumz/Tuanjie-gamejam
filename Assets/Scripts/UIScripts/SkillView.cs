using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillView : MonoBehaviour
{
    // Start is called before the first frame update
    public BaseSkill skill;
    public Sprite skillImage;
    public string skillName;
    public string skillRemark;

    public void Init(BaseSkill skill)
    {
        // GetComponentInChildren<Image>().sprite = null;
        this.skill = skill;
        GetComponentInChildren<Text>().text = skill.skillName + ":" + skill.remark;
    }
}
