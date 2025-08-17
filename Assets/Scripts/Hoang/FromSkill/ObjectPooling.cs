using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> pooledObjectsPlayer;
    public GameObject objectToPoolPlayer;
    public int amountToPoolPlayer;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjectsPlayer = new List<GameObject>();
        GameObject tmpPlayer;
        for (int i = 0; i < amountToPoolPlayer; i++)
        {
            tmpPlayer = Instantiate(objectToPoolPlayer);
            tmpPlayer.SetActive(false);
            pooledObjectsPlayer.Add(tmpPlayer);           
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPoolPlayer; i++)
        {
            if (!pooledObjectsPlayer[i].activeInHierarchy)
            {
                return pooledObjectsPlayer[i];
            }
        }
        return null;
    }
}
