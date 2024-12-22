using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
 
    private static SkillPool _instance;
    public static SkillPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SkillPool>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SkillPool");
                    _instance = obj.AddComponent<SkillPool>();
                }
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    //通用技能池
    public List<BaseSkill> normalSkill;
    //当局游戏技能池
    public List<BaseSkill> skillPool;
    //技能组池
    public List<List<BaseSkill>> skillGroup;
    //玩家所拥有技能
    public List<BaseSkill> playerSkill;
    
    //放到一个列表里
    //根据结算阶段去区分 再各个阶段去遍历所分的结算列表 遍历执行
    //抽牌后结算
    private List<BaseSkill> drawedCardSettle;
    //测试方法调用

    public void OnEnable()
    {
        foreach (var skill in normalSkill)
        {
            foreach (var skilleff in skill.skillEffect)
            {
                skilleff.subscribeEvent();
            }
        }
    }

    public void OnDisable()
    {
        foreach (var skill in normalSkill)
        {
            foreach (var skilleff in skill.skillEffect)
            {
                skilleff.unsubscribeEvent();
            }
        }
    }

}
