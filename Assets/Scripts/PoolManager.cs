using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
  
    public Transform pool;
    public List<GameObject> availableObjects = new List<GameObject>();
    public List<GameObject> inGameObjects = new List<GameObject>();

    void Awake()
    {
        foreach (Transform child in pool)
        {
            availableObjects.Add(child.gameObject);
        }
    }

    public GameObject getRandomObject() {
        GameObject go = availableObjects[Random.Range(0, availableObjects.Count - 1)];
        availableObjects.Remove(go);
        inGameObjects.Add(go);
        return go;
    }

    public void returnToPool(GameObject go) {
        go.transform.position = pool.position;
        inGameObjects.Remove(go);
        availableObjects.Add(go);
    }

    public void standBy(GameObject go) {
        go.transform.position = pool.position;
        inGameObjects.Remove(go);
    }

    public GameObject[] getInGameObj() {
        GameObject[] aux = new GameObject[inGameObjects.Count];
        inGameObjects.CopyTo(aux);
        return aux;
    }

    public void returnAll() {
        GameObject[] aux = new GameObject[inGameObjects.Count];
        inGameObjects.CopyTo(aux);
        foreach (GameObject go in aux) {
            returnToPool(go);
        }
    }

}
