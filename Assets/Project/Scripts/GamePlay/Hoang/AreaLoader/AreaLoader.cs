using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    public static AreaLoader instance;
    private void OnEnable()
    {
        instance = this;
        areaList = new Dictionary<int, Area>();
    }
    private void OnDisable()
    {
        instance = null;
    }
    public int currentAreaId;
    public Dictionary<int, Area> areaList;   

    public void LoadArea()
    {
        List<int> areaIdActive = new List<int>();
        areaIdActive.Add(currentAreaId);
        foreach (var area in areaList[currentAreaId].areaNearBy)
        {
            areaIdActive.Add(area.areaId);
        }
        foreach(var area in areaList)
        {
            if (areaIdActive.Contains(area.Key))
            {
                area.Value.gameObject.SetActive(true);
            }
            else
            {
                area.Value.gameObject.SetActive(false);
            }
        }
    }
}
