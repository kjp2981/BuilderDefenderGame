using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;

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
        index++;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            
            btnTransform.GetComponent<Button>().onClick.AddListener(() => BuildingManager.Instance.SetActiveBuildingType(buildingType));

            btnTransformDic[buildingType] = btnTransform;

            index++;
        }
    }

    private void Update()
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
