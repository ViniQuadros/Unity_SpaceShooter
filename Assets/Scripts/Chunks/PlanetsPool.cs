using System.Collections.Generic;
using UnityEngine;

public class PlanetsPool : MonoBehaviour
{
    public static PlanetsPool Instance { get; private set; }

    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private int poolSize = 30;

    private Queue<GameObject> pool = new();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(planetPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Pool exhausted — expand it
        GameObject newObj = Instantiate(planetPrefab);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(null);
        pool.Enqueue(obj);
    }
}