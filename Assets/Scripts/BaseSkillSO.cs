using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/Skill")]
public class BaseSkillSO : ScriptableObject
{
    public string SkillName;
    public float Range;
    public float RangeOffset;
    public float Delay;
    public float Duration;
    public Sprite Sprite;
    public GameObject EffectPrefab;

    public ResourceAmount[] CostArray;

    public string GetConstructionCostString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in CostArray)
        {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" +
                resourceAmount.resourceType.nameShort + resourceAmount.amount
                + "</color>";
        }

        return str;
    }
}
