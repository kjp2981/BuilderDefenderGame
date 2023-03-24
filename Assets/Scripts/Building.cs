using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        Debug.Log(healthSystem.GetHealthAmount());
        healthSystem.Damage(10);
        Debug.Log(healthSystem.GetHealthAmount());
        healthSystem.Damage(50);
        Debug.Log(healthSystem.GetHealthAmount());
        healthSystem.Damage(40);
        Debug.Log(healthSystem.GetHealthAmount());
        Debug.Log(healthSystem.IsDead());
    }
}
