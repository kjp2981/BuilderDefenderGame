using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private List<BaseSkill> _skillList = new List<BaseSkill>();
    public List<BaseSkill> SkillList => _skillList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddSkill(new ExplosionSkill());

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(_skillList[0].SkillActionCoroutine(UtilClass.GetMouseWorldPosition()));
        }
    }

    public void AddSkill(BaseSkill skill)
    {
        _skillList.Add(skill);
    }
}
