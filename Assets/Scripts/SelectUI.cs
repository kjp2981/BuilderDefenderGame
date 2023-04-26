using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour
{
    public static SelectUI Instance { get; private set; }

    [SerializeField]
    private List<GameObject> _selectUIList = new List<GameObject>();

    int index = 0;

    private void Awake()
    {
        Instance = this;
    }

    public List<GameObject> GetSelectList()
    {
        return _selectUIList;
    }

    public void NextSelectUI()
    {
        _selectUIList[index].SetActive(false);
        index = (index + 1) % _selectUIList.Count;
        _selectUIList[index].SetActive(true);
    }
}
