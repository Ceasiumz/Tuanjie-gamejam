using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillList : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _listEntryTemplate;
    [SerializeField] private int _itemHeight;
    [SerializeField] private int _scrollSize;

    private List<BaseSkill> _skillList;

    private VisualElement _root;
    private VisualElement _base;
    private ListView _listView;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _base = _root.Query<VisualElement>("Base");

        _listView = _root.Query<ListView>();

        ScrollView scrollView = _listView.Query<ScrollView>();
        scrollView.mouseWheelScrollSize = _scrollSize;

        InitializeSkillList();


        SkillPool.Instance.OnSkillListButtonDown += OnShowList;
    }

    private void OnShowList()
    {
        
        if (_base.style.visibility == Visibility.Visible)
        {
            _base.style.visibility = Visibility.Hidden;
        }
        else
        {
            
            _base.style.visibility = Visibility.Visible;
        }
    }

    private void OnGUI()
    {
        _skillList = SkillPool.Instance.playerSkill;
        _listView.itemsSource = _skillList;
        _listView.Rebuild();
    }

    private void InitializeSkillList()
    {
        Action<VisualElement, int> bindItem = (item, i) =>
        {
            (item.userData as SkillListEntry).SetSKillData(_skillList[i]);
        };


        _listView.itemsSource = _skillList;
        _listView.fixedItemHeight = _itemHeight;
        _listView.makeItem = OnMakeItem;
        _listView.bindItem = bindItem;


        _listView.selectionType = SelectionType.Single;

        _listView.itemsChosen += objs => { };

        _listView.selectionChanged += OnSelectionChanged;

        _listView.style.flexGrow = 1;
        _base.Add(_listView);
    }

    private void OnSelectionChanged(IEnumerable<object> objs)
    {
        BaseSkill skill = objs.ElementAt(0) as BaseSkill;
        SkillPool.Instance.ExecuteSkill(skill);
    }

    private VisualElement OnMakeItem()
    {
        var listEntry = _listEntryTemplate.Instantiate();
        var skillListEntry = new SkillListEntry();
        skillListEntry.SetVisualElement(listEntry);
        listEntry.userData = skillListEntry;
        return listEntry;
    }
}
