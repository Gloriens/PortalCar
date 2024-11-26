using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMap : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            transform.parent.position += new Vector3(2 * -transform.parent.GetComponent<Renderer>().bounds.size.x, 0, 0);
        }
        
    }
}
