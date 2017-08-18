using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    public GameObject carPrefab;

    public int numOfCars;

	// Use this for initialization
	void Start ()
    {

	}

    [ContextMenu("Spawn Cars")]
    void SpawnCars()
    {
        for (int i = 0; i < numOfCars; i++)
        {
            RoadPoint p = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<RoadPoint>();

            GameObject obj = Instantiate(carPrefab, p.transform.position, Quaternion.identity);
            obj.GetComponent<Car>().currentPoint = p;
            obj.GetComponent<Car>().nextPoint = p.connected[Random.Range(0, p.connected.Count)];
        }
    }
}
