using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public List<SkillGroup> skillGroup;
    //玩家所拥有技能
    public List<BaseSkill> playerSkill;

    public List<BaseSkill> skillTestPool;
    
    public SkillSelect skillSelectDialog;
    
    //备注： 开始时应初始化 skillpool当局技能池为本轮游戏可获得技能
    //游戏开局时应在技能组池中选择一组技能加入到当局游戏技能池中 通用技能池中技能也应加入到当局游戏技能池中

    
    //从技能池中随机获取三个不重复技能
    public List<BaseSkill> GetThreeSkill()
    {
        List<BaseSkill> result = new List<BaseSkill>();
        List<BaseSkill> availableSkills = skillPool.Where(skill => !skill.isHidden).ToList();

        if (availableSkills.Count < 3)
        {
            // 如果可用技能少于3个，直接返回所有可用技能
            return availableSkills;
        }

        while (result.Count < 3)
        {
            int index = UnityEngine.Random.Range(0, availableSkills.Count);
            BaseSkill skill = availableSkills[index];
            if (!result.Contains(skill))
            {
                result.Add(skill);
            }
        }
        return result;
    }
    
    //初始化本局游戏技能池
    public void InitSkillPool(string skillGroupID)
    {
        skillPool = new List<BaseSkill>();
        skillPool.AddRange(normalSkill);
        skillPool.AddRange(skillGroup.Find(x => x.skillGroupId == skillGroupID).skills);
    }
    
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
            x.ImmediateTrigger();
        });

        if (skill.skillType == SkillType.Active)
        {
            skill.subscribeTurnEvent();
        }
        skillSelectDialog.hideDialog();
    }
    
    //玩家技能组移除技能
    public void RemovePlayerSkill(BaseSkill skill)
    {
        skill.skillEffect.ForEach(x => x.unsubscribeEvent());
        if (skill.skillType == SkillType.Active)
        {
            skill.unsubscribeTurnEvent();
        }
        playerSkill.Remove(skill);
        
    }
    
    //执行技能
    public void ExecuteSkill(BaseSkill skill)
    {
        skill.skillEffect.ForEach(x => x.Execute());
        MouseManager.Instance.isReleaseSkill = true;
        //技能使用后返回技能组池中判定
        //在卡牌技能效果中实现
        // if (skill.isUsedReturn)
        // {
        //     skillPool.Add(skill);
        // }
        // if(skill.isOneTime)
        // {
        //     playerSkill.Remove(skill);
        // }

    }
    
    //技能执行中断
    public void InterruptSkill(BaseSkill skill)
    {
        skill.skillEffect.ForEach(x => x.Interrupt());
    }
    
    
    //将技能放回进技能池中
    public void ReturnSkill(BaseSkill skill)
    {
        skillPool.Add(skill);
    }
    //根据ID将技能返回技能池
    public void ReturnSkillFromPlayerSkill(string skillID)
    {
        skillPool.Add(playerSkill.Find(x => x.skillID == skillID));
    }
    
    //根据技能ID从玩家技能列表中移除技能
    public void RemovePlayerSkillByID(string skillID)
    {
        //从玩家技能列表中找到id为skillID的技能
        BaseSkill skill = playerSkill.Find(x => x.skillID == skillID);
        skill.skillEffect.ForEach(x => x.unsubscribeEvent());
        if (skill.skillType == SkillType.Active)
        {
            skill.unsubscribeTurnEvent();
        }
        playerSkill.Remove(skill);
    }
    
    //根据id从通用技能池中查找技能
    public BaseSkill FindSkillByID(string skillID)
    {
        return skillPool.Find(x => x.skillID == skillID);
    }
    
    //以下为测试代码 此代码将skillTestPool中技能默认设置为激活
    public void OnEnable()
    {
        InitSkillPool("A2");
        // foreach (var skill in skillTestPool)
        // {
        //     foreach (var skilleff in skill.skillEffect)
        //     {
        //         skilleff.subscribeEvent();
        //     }
        // }
        // AddPlayerSkill(skillTestPool[0]);
        
    }
    

    public void OnDisable()
    {
        // foreach (var skill in skillTestPool)
        // {
        //     foreach (var skilleff in skill.skillEffect)
        //     {
        //         skilleff.unsubscribeEvent();
        //     }
        // }
        // RemovePlayerSkill(skillTestPool[0]);
    }

    public void RoundStart()
    {
        
        for (int i = playerSkill.Count - 1; i >= 0; i--)
        {
            RemovePlayerSkill(playerSkill[i]);
        }

        InitSkillPool("A2");
    }

}
