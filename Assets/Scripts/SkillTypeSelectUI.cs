using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite sprite;

    private BaseSkill[] skillList;
    private Dictionary<BaseSkill, Transform> btnTransformDic;
    Transform arrowBtn;

    private void Start()
    {
        skillList = SkillManager.Instance.GetSkillList();
        btnTransformDic = new Dictionary<BaseSkill, Transform>();


        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        int index = 0;

        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        float offsetAmount = +130f;
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowBtn.Find("image").GetComponent<Image>().sprite = sprite;
        arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowBtn.GetComponent<Button>().onClick.AddListener(() => {
            SkillManager.Instance.SetSelectSkill(null);
            SelectUI.Instance.NextSelectUI();
        });

        MouseEnterExitEvents mouseEnterExitEvents = arrowBtn.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Change Build");
        };

        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };


        index++;

        foreach (BaseSkill skill in skillList)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = skill.SkillSO.Sprite;


            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                SkillManager.Instance.SetSelectSkill(skill);
            });

            mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(skill.SkillSO.SkillName + "\n" + skill.SkillSO.GetConstructionCostString());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };


            btnTransformDic[skill] = btnTransform;
            index++;
        }

        UpdateSelectSkillBtn();
        //BuildingManager.Instance.onActiveBuildingTypeChanged += SkillManager_onActiveBuildingTypeChanged;
        SkillManager.Instance.onSkillChanged += SkillManager_onSkillChanged;
    }

    private void SkillManager_onSkillChanged(object sender, SkillManager.onSkillEventArgs e)
    {
        UpdateSelectSkillBtn();
    }

    private void UpdateSelectSkillBtn()
    {
        arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (BaseSkill skill in btnTransformDic.Keys)
        {
            Transform btnTransform = btnTransformDic[skill];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BaseSkill activeBuildingType = SkillManager.Instance.GetSelectSkill();


        if (activeBuildingType == null)
        {
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            Transform activeBtnTransform = btnTransformDic[activeBuildingType];
            activeBtnTransform.Find("selected").gameObject.SetActive(true);
        }
    }
}
