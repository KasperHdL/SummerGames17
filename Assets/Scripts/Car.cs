using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public RoadPoint currentPoint, nextPoint;
    private float distance;
    private float t;
    public float speed;

	// Use this for initialization
	void Start () {
        t = 0f;
        distance = Vector3.Distance(currentPoint.transform.position, nextPoint.transform.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        t += (1 / distance) * speed * Time.deltaTime;
        transform.position = Vector3.Lerp(currentPoint.transform.position, nextPoint.transform.position, t);


        float step = 4f * Time.deltaTime;
        Vector3 target = nextPoint.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (t >= 1) NextPoint();
	}

    void NextPoint()
    {
        currentPoint = nextPoint;
        nextPoint = currentPoint.connected[Random.Range(0, nextPoint.connected.Count)];
        distance = Vector3.Distance(currentPoint.transform.position, nextPoint.transform.position);
        t = 0f;
    }
}


