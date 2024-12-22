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

    [SerializeField] private List<SkillDataTemp> _skillList;

    private VisualElement _root;
    private VisualElement _base;
    private ListView _listView;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _base = _root.Query<VisualElement>("Base");

        _listView = _root.Query<ListView>();

        InitializeSkillList();

        _root.visible = false;
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

        _listView.itemsChosen += objs => Debug.Log(objs.ElementAt(0));

        _listView.selectionChanged += objs => Debug.Log(objs.ElementAt(0));

        _listView.style.flexGrow = 1;
        _base.Add(_listView);
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
