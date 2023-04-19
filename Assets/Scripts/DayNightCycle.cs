using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private Gradient gradient;

    private Light2D light2D;

    [SerializeField]
    private float secondsPerDay = 10f;
    private float dayTime;
    private float dayTimerSpeed;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        dayTimerSpeed = 1 / secondsPerDay;
    }

    private void Update()
    {
        dayTime += Time.deltaTime * dayTimerSpeed;
        light2D.color = gradient.Evaluate(dayTime % 1f);
    }
}
