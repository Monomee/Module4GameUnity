using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPooling : MonoBehaviour
{
    public List<GameObject> pooledObjectsEnemy;
    public GameObject objectToPoolEnemy;
    public int amountToPoolEnemy;

    void Start()
    {
        pooledObjectsEnemy = new List<GameObject>();
        GameObject tmpPlayer;
        for (int i = 0; i < amountToPoolEnemy; i++)
        {
            tmpPlayer = Instantiate(objectToPoolEnemy);
            tmpPlayer.SetActive(false);
            pooledObjectsEnemy.Add(tmpPlayer);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPoolEnemy; i++)
        {
            if (!pooledObjectsEnemy[i].activeInHierarchy)
            {
                return pooledObjectsEnemy[i];
            }
        }
        return null;
    }
}
