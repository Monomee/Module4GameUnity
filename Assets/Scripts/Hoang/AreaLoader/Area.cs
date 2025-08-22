using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    BoxCollider box;
    public int areaId;
    public List<Area> areaNearBy;
    public List<UnitBase> enemyInArea;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        Vector3 boxCenter = box.transform.TransformPoint(box.center);
        Vector3 halfExtents = box.size * 0.5f;
        Quaternion boxOrientation = box.transform.rotation;

        Collider[] hits = Physics.OverlapBox(boxCenter, halfExtents, boxOrientation);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("CanTakeDmg"))
            {
                enemyInArea.Add(hit.gameObject.GetComponent<UnitBase>());
            }
        }

        AreaLoader.instance.areaList.Add(areaId, this);
    }

    public void ActiveArea()
    {
        this.gameObject.SetActive(true);
        foreach (UnitBase unit in enemyInArea)
        {
            unit.gameObject.SetActive(true);
        }
    }
    public void DeactiveArea()
    {
        foreach (UnitBase unit in enemyInArea)
        {
            unit.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AreaLoader.instance.currentAreaId = areaId;
            AreaLoader.instance.LoadArea();
        }
    }
}
