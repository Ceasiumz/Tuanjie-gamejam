using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skilltab_ShowSkill : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject playerSkill;
    private List<BaseSkill> skillList;
    public Transform skillListParent;
    private void OnEnable()
    {
        InitDialog();
    }
    public void InitDialog()
    {
        ClearPreviousObjects();
        skillList = SkillPool.Instance.playerSkill;
        int i = 0;
        foreach (var skill in skillList)
        {
            GameObject obj = Instantiate(playerSkill, scrollRect.content);
            i++;
            obj.GetComponent<SkillView>().Init(skill);
        }
    }
    private void ClearPreviousObjects()
    {
        //获取skillListParent下的所有子对象
        int childCount = skillListParent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = skillListParent.GetChild(i);
            // 销毁每个子对象
            Destroy(child.gameObject);
        }
    }

}
