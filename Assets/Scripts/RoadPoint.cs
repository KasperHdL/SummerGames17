using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class RoadPoint : MonoBehaviour {
	public List<RoadPoint> connected = new List<RoadPoint>();

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    #if UNITY_EDITOR
    // This is some disgusting code WOW
    // I AM SO SORRY I HAVEN'T DONE SOME PROPER SHIT IN A WHILE
    [ContextMenu("Create Point")]
    public void Create() 
	{
        GameObject p = new GameObject("Point");
        Selection.activeGameObject = p;
        p.transform.position = transform.position;
        p.AddComponent<RoadPoint>();
        connected.Add(p.GetComponent<RoadPoint>());

	}
#endif

    public void Delete()
    {
        foreach(RoadPoint p in connected)
        {
            p.connected.Remove(this);
        }

        DestroyImmediate(gameObject);
    }

    public void Merge()
    {
        //TODO Implement merging with nearest Point
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.4f);
        if(connected.Count > 0)
            foreach(RoadPoint p in connected)
            {
                if (p == null) continue;
                Gizmos.DrawLine(transform.position, p.gameObject.transform.position);
            }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (connected.Count > 0)
            foreach (RoadPoint p in connected)
            {
                if (p == null) continue;
                Gizmos.DrawLine(transform.position, p.gameObject.transform.position);
            }
    }
}