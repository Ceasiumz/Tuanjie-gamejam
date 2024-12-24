using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    
    //是否在释放技能
    public bool isReleaseSkill=false;
    //是否可以选择敌人卡牌
    public bool CanSelectEnemyCard=false;
    //被选择的卡牌
    public List<Card> selectedCards;
    
    //选择卡牌数量
    public int selectCardNum=0;
    
    private static MouseManager _instance;
    public static MouseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MouseManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GamePointBoard");
                    _instance = obj.AddComponent<MouseManager>();
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

    public void SkillExecute(List<Card> cards)
    {
        Debug.Log("SkillExecute");
        
        DynamicEventBus.Publish("SkillExecute", cards);
        isReleaseSkill = false;
        selectedCards.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
