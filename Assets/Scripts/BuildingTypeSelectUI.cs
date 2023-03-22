using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private List<BuildingTypeSO> ignoreBuildingTypeList;

    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> btnTransformDic;

    private Transform arrowBtn;

    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        btnTransformDic = new Dictionary<BuildingTypeSO, Transform>();

        Transform btnTemplate = transform.Find("BtnTemplate");
        btnTemplate.gameObject.SetActive(false);

        int index = 0;
        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        float offsetAmount = 160f;
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowBtn.Find("Image").GetComponent<Image>().sprite = sprite;
        arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);
        arrowBtn.GetComponent<Button>().onClick.AddListener(() => BuildingManager.Instance.SetActiveBuildingType(null));

        MouseEnterExitEvents mouseEnterExitEvents = arrowBtn.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow Button");
        };

        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };

        index++;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            
            btnTransform.GetComponent<Button>().onClick.AddListener(() => BuildingManager.Instance.SetActiveBuildingType(buildingType));

            mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString + "\n" +  buildingType.GetConstructionCost());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            btnTransformDic[buildingType] = btnTransform;

            index++;
        }
    }

    private void Start()
    {
        UpdateActiveBuildingTypeBtn();
        BuildingManager.Instance.onActivelBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
    }

    private void BuildingManager_onActiveBuildingTypeChanged(object sender, BuildingManager.onActiveBuildingTypeEventArgs e)
    {
        UpdateActiveBuildingTypeBtn();
    }

    private void UpdateActiveBuildingTypeBtn()
    {
        arrowBtn.Find("Selected").gameObject.SetActive(false);

        foreach(BuildingTypeSO buildingType in btnTransformDic.Keys)
        {
            Transform btnTransform = btnTransformDic[buildingType];
            btnTransform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if(activeBuildingType == null)
        {
            arrowBtn.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            Transform activeBtnTransform = btnTransformDic[activeBuildingType];
            activeBtnTransform.Find("Selected").gameObject.SetActive(true);
        }
    }
}
