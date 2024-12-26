using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SkillSelectButton : MonoBehaviour
{

    public BaseSkill skill;
    public Text skillName;
    public Text skillDescription;
    public Image SkillImage;
    public AudioSource SkillAudioSource;

    public void Init(BaseSkill skill)
    {

        this.skill = skill;
        //GetComponentInChildren<Image>().sprite = skill.skillImage;
        SkillImage.sprite = skill.skillImage;
        skillName.text = skill.skillName;
        skillDescription.text = skill.remark;
    }
    public void ClickSound()
    {
        SkillAudioSource.Play();
    }
    public void AddSkill()
    {
        SkillPool.Instance.AddPlayerSkill(skill);
        Transform.Destroy(gameObject);
    }
}
