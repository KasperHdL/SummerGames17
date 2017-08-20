using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour {
    public GameObject flash;
    public AudioClip[] sounds;


    void OnTriggerStay(Collider other)
    {
        SwarmClient client = other.GetComponent<SwarmClient>();
        if (client == null) return;

        // fair roll of dice
        if(Random.Range(0, 500) >= 499) {
            GameObject obj = Instantiate(flash, client.transform.position + (Vector3.up * 2), Quaternion.identity);
            Destroy(obj, 0.3f);
            AudioSource.PlayClipAtPoint(sounds[Random.Range(0, sounds.Length)], other.transform.position);
        }
    }
}
