using System;
using System.Collections.Generic;

public static class DynamicEventBus
{
    // 使用字典存储事件，键是事件名称，值是委托
    private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    // 动态订阅事件（支持泛型）
    public static void Subscribe<T>(string eventName, Action<T> listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] = Delegate.Combine(eventTable[eventName], listener);
        }
        else
        {
            eventTable[eventName] = listener;
        }
    }

    // 动态取消订阅事件（支持泛型）
    public static void Unsubscribe<T>(string eventName, Action<T> listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] = Delegate.Remove(eventTable[eventName], listener);
            if (eventTable[eventName] == null)
            {
                eventTable.Remove(eventName);
            }
        }
    }

    // 动态触发事件（支持泛型）
    public static void Publish<T>(string eventName, T eventData)
    {
        if (eventTable.ContainsKey(eventName))
        {
            if (eventTable[eventName] is Action<T> callback)
            {
                callback.Invoke(eventData);
            }
            else
            {
                throw new Exception($"Event {eventName} has a mismatched parameter type.");
            }
        }
    }
    
    // 订阅无参数事件
    public static void Subscribe(string eventName, Action listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] = Delegate.Combine(eventTable[eventName], listener);
        }
        else
        {
            eventTable[eventName] = listener;
        }
    }

    // 取消订阅无参数事件
    public static void Unsubscribe(string eventName, Action listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] = Delegate.Remove(eventTable[eventName], listener);
            if (eventTable[eventName] == null)
            {
                eventTable.Remove(eventName);
            }
        }
    }
    
    // 触发无参数事件
    public static void Publish(string eventName)
    {
        if (eventTable.ContainsKey(eventName))
        {
            if (eventTable[eventName] is Action callback)
            {
                callback.Invoke();
            }
            else
            {
                throw new Exception($"Event {eventName} is not a parameterless event.");
            }
        }
    }
    
    // 动态创建事件名称（便于动态化管理）
    public static string CreateEventName(string baseName, params object[] parameters)
    {
        return $"{baseName}_{string.Join("_", parameters)}";
    }
}