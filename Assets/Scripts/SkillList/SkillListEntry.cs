using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillListEntry
{
    private VisualElement _icon;
    private Label _name;
    public void SetVisualElement(VisualElement visualElement)
    {
        _icon=visualElement.Query<VisualElement>("Icon");
        _name=visualElement.Query<Label>("SkillName");
    }

    public void SetSKillData(BaseSkill data)
    {
        _name.text=data.skillName;
        //_icon.style.backgroundImage=data.SkillIcon;
    }
}
