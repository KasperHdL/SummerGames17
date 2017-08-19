using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuild : MonoBehaviour {
    public GameObject[] objs;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    [ContextMenu("DELETE")]
    void Delete() {
        if(objs.Length > 0)
            foreach(GameObject obj in objs) {
                DestroyImmediate(obj);
            }
    }

    [ContextMenu("Do Something")]
    void Generate()
    {
        Delete();

        // Create Buildings
        // wsize = 500x500
        // bsize = 20x20
        // t_house = 25x25
        objs = new GameObject[25*25];

        for(int x = 0; x < 25;x++)
        {
            for(int y = 0; y < 25;y++)
            {
                objs[x + y * 25] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                objs[x + y * 25].name = "Building" + (x + y * 25);
                objs[x + y * 25].transform.parent = transform;
                float height = 10 + (Mathf.PerlinNoise((float)x * 0.1f, (float)y * 0.3f) * 35f) + Random.Range(2,13);
                objs[x + y * 25].transform.localScale = new Vector3(18,height,18);
                objs[x + y * 25].transform.position = new Vector3((x * 20)-240, height/2, (y * 20)-240);
            }
        }

        // Create roads

    }
}
