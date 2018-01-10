using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indexed_Distance : MonoBehaviour {

    public float index;

    private void OnTriggerEnter(Collider other)
    {
        print("message sent");
        
            other.gameObject.GetComponent<Agent>().UpDateDistance(index);
        
    }




}
