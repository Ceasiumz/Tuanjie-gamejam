using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseEffect:ScriptableObject
{
    //特定流程触发技能
    public abstract void subscribeEvent();
    public abstract void unsubscribeEvent();
    
    //主动技能执行
    public abstract void Execute();
    
    //立刻触发效果 
    public abstract void ImmediateTrigger();
    
}
