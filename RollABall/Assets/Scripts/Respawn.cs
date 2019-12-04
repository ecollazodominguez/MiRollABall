using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

public float limite;

void FixedUpdate () {
         if (transform.position.y < limite)
             transform.position = new Vector3(0, 0, 0);
     }
 
}
