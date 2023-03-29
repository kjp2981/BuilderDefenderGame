using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public event EventHandler<onActiveBuildingTypeEventArgs> onActivelBuildingTypeChanged;

    public class onActiveBuildingTypeEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField]
    private Building hqBuilding;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, UtillClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostArray))
                    {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionCostArray);
                        Instantiate(activeBuildingType.prefab, UtillClass.GetMouseWorldPosition(), Quaternion.identity);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("자원 부족 : " + activeBuildingType.GetConstructionCost());
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage);
                }
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        onActivelBuildingTypeChanged?.Invoke(this, new onActiveBuildingTypeEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "건물을 놓을 수 없는 위치입니다.";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    errorMessage = "같은 유형의 건물이 주변에 있습니다.";
                    return false;
                }
            }
        }

        float maxCinstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxCinstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "주변에 건물이 있어야 합니다.";
        return false;
    }

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
