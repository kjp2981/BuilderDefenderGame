using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtillClass
{
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        Vector3 mouserWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouserWorldPosition.z = 0;
        return mouserWorldPosition;
    }
}
