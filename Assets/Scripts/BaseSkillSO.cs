using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill")]
public class BaseSkillSO : ScriptableObject
{
    public string SkillName;
    public float Range;
    public float Delay;
    public float Duration;
    public GameObject EffectPrefab;

    public ResourceAmount[] CostArray;
}
