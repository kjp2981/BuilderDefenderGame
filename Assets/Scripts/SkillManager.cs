using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using static BuildingManager;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private List<BaseSkill> _skillList = new List<BaseSkill>();

    private BaseSkill _selectSkill;

    public event EventHandler<onSkillEventArgs> onSkillChanged;

    public class onSkillEventArgs : EventArgs
    {
        public BaseSkill skill;
    }

    private void Awake()
    {
        Instance = this;

        AddSkill(new ExplosionSkill());
        AddSkill(new SlowSkill());

        Init();
    }

    public void Init()
    {
        foreach (BaseSkill skill in _skillList)
        {
            skill.Init();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_selectSkill != null)
            {
                if (ResourceManager.Instance.CanAfford(_selectSkill.SkillSO.CostArray))
                {
                    ResourceManager.Instance.SpendResources(_selectSkill.SkillSO.CostArray);
                    StartCoroutine(_selectSkill.SkillActionCoroutine(UtilClass.GetMouseWorldPosition()));
                    //SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                }
                else
                {
                    TooltipUI.Instance.Show("자원부족 : " + _selectSkill.SkillSO.GetConstructionCostString(), new TooltipUI.Timer { timer = 2f });
                }
            }

        }
    }

    public void AddSkill(BaseSkill skill)
    {
        _skillList.Add(skill);
    }

    public BaseSkill GetSelectSkill()
    {
        return _selectSkill;
    }

    public void SetSelectSkill(BaseSkill skill)
    {
        _selectSkill = skill;
        onSkillChanged?.Invoke(this, new onSkillEventArgs { skill = _selectSkill });
    }

    public BaseSkill[] GetSkillList()
    {
        return _skillList.ToArray();
    }
}
