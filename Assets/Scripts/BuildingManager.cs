using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO buildingType;

    private Camera mainCamera;

    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.list[0];
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            buildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            buildingType = buildingTypeList.list[1];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            buildingType = buildingTypeList.list[2];
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouserWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouserWorldPosition.z = 0;
        return mouserWorldPosition;
    }
}
