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
    
    //备注： 开始时应初始化 skillpool当局技能池为本轮游戏可获得技能
    //游戏开局时应在技能组池中选择一组技能加入到当局游戏技能池中 通用技能池中技能也应加入到当局游戏技能池中

    //玩家技能组添加技能
    public void AddPlayerSkill(BaseSkill skill)
    {
        playerSkill.Add(skill);
        //当技能可重复获得时不从技能池中移除技能
        if (!skill.isRepeatable)
        {
            skillPool.Remove(skill);
        }
        //被动技能应立即触发效果
        skill.skillEffect.ForEach(x =>
        {
            x.subscribeEvent();
            x.PropertyChange();
        });
    }
    
    //玩家技能组移除技能
    public void RemovePlayerSkill(BaseSkill skill)
    {
        playerSkill.Remove(skill);
        skill.skillEffect.ForEach(x => x.unsubscribeEvent());
    }
    
    //执行技能
    public void ExecuteSkill(BaseSkill skill)
    {
        skill.skillEffect.ForEach(x => x.Execute());
        playerSkill.Remove(skill);
        //技能使用后返回技能组池中判定
        if (skill.isUsedReturn)
        {
            skillPool.Add(skill);
        }
    }
    
    //将技能放回进技能组池中
    public void ReturnSkill(BaseSkill skill)
    {
        skillGroup.Add(skillPool);
    }
    
    
    //以下为测试代码 此代码将normalskill中技能默认设置为激活
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
