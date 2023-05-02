using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Resources.Load<GameAssets>("GameAssets");
            }
            return instance;
        }
    }

    public Transform pfEnemy;
    public Transform pfBossEnemy;
    public Transform pfEnemyDieParticles;
    public Transform pfArrowProjectile;
    public Transform pfBuildingDestroyParticlse;
    public Transform pfBuildingConsturction;
    public Transform pfBuildingPlacedParticles;

}
