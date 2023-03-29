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

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1), 1).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
}
