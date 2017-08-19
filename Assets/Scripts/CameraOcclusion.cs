using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOcclusion : MonoBehaviour {

    public Transform camera;
    public Transform guide;

    public AnimationCurve cutoff;

    private MeshRenderer[] buildings;


    public void Start(){
        buildings = new MeshRenderer[transform.childCount];

        int i = 0;
        foreach(Transform child in transform){
            buildings[i] = child.GetComponent<MeshRenderer>();


            i++;
        }

    }

    public void Update(){
        Vector3 delta = camera.position - guide.position;
        delta.y = 0;
        delta = delta.normalized;

        for(int i = 0; i < buildings.Length; i++){

            Vector3 b = buildings[i].transform.position - guide.position;
            b.y = 0;
            b = b.normalized;

            Debug.DrawRay(buildings[i].transform.position, b, Color.red);
            Debug.DrawRay(buildings[i].transform.position, delta, Color.green);

            
            float d = Vector3.Dot(delta, b);
            
            if(d >= 0){

                buildings[i].material.SetFloat("_Transparency", cutoff.Evaluate(d));

            }





        }

    }
}
