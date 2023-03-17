using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
    }

    private void Start()
    {
        resourceNearbyOverlay = transform.Find("ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();

        Hide();
        BuildingManager.Instance.onActivelBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
    }

    private void BuildingManager_onActiveBuildingTypeChanged(object sender, BuildingManager.onActiveBuildingTypeEventArgs e)
    {
        if(e.buildingType == null)
        {
            resourceNearbyOverlay.Hide();
            Hide();
        }
        else
        {
            resourceNearbyOverlay.Show(e.buildingType.resourceGeneratorData);
            Show(e.buildingType.sprite);
        }
    }

    private void Update()
    {
        transform.position = UtillClass.GetMouseWorldPosition();
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
}
