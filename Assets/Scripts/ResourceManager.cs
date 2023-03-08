using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    [SerializeField]
    private List<TextMeshProUGUI> texts;
    private Dictionary<ResourceTypeSO, TextMeshProUGUI> resourceTextDictionary;

    void Awake()
    {
        Instance = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        resourceTextDictionary = new Dictionary<ResourceTypeSO, TextMeshProUGUI>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach(ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }
        for(int i = 0; i < resourceTypeList.list.Count; i++)
        {
            resourceTextDictionary[resourceTypeList.list[i]] = texts[i];
        }

        TestLogResourceAmountDictionary();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceTypeList.list[0], 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceTypeList.list[1], 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceTypeList.list[2], 1);
        }
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach(ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + " : " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        resourceTextDictionary[resourceType].SetText(resourceAmountDictionary[resourceType].ToString());
#if UNITY_EDITOR
        TestLogResourceAmountDictionary();
#endif
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
