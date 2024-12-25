using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    public GameObject SkillSelectButton;
    public Transform skillListParent;
    // public GameObject obj;
    private List<BaseSkill> skillList;

    public void showDialog()
    {
        this.GameObject().SetActive(true);
    }
    public void hideDialog()
    {
        this.GameObject().SetActive(false);
    }

    public void InitDialog()
    { skillList= SkillPool.Instance.GetThreeSkill();
        int i = 0;
       foreach (var skill in skillList)
       {
            GameObject obj = Instantiate(SkillSelectButton,this.transform);
            // obj.transform.SetParent(transform);
            obj.transform.position = new Vector3(skillListParent.position.x, skillListParent.position.y- i * 2 ,
                skillListParent.position.z);
            i++;
           obj.GetComponent<SkillSelectButton>().Init(skill);
       }
    }

    public void OnEnable()
    {
        InitDialog();
    }

    public void OnDisable()
    {
        //销毁之前创建的技能选择按钮
        foreach (Transform child in transform)
        {
            if(child.name!="Transform")
                Destroy(child.gameObject);
        }
    }
}
