using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;

    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
    }

    private void Start()
    {
        Hide();
        BuildingManager.Instance.onActivelBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
    }

    private void BuildingManager_onActiveBuildingTypeChanged(object sender, BuildingManager.onActiveBuildingTypeEventArgs e)
    {
        if(e.buildingType == null)
        {
            Hide();
        }
        else
        {
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
