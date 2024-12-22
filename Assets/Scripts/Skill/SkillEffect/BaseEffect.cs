using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseEffect:ScriptableObject
{
    public abstract void subscribeEvent();
    public abstract void unsubscribeEvent();

}
