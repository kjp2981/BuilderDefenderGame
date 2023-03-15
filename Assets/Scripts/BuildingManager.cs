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
        public BuildingTypeSO buildingType;
    }
    
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
                Instantiate(activeBuildingType.prefab, UtillClass.GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        onActivelBuildingTypeChanged?.Invoke(this, new onActiveBuildingTypeEventArgs { buildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
