using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

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
            if(activeBuildingType != null)
            {
                Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouserWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouserWorldPosition.z = 0;
        return mouserWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
