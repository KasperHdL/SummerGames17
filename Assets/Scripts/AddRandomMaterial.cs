using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomMaterial : MonoBehaviour {
    public Material[] materials;

    [ContextMenu("Add Random Materials")]
    void AddMats()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
        }
    }
}
