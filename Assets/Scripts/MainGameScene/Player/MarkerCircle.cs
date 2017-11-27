using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCircle : SceneStartEvent{

    float starty;

	// Use this for initialization
	void Start () {
        starty = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = new Vector3(transform.position.x, starty, transform.position.z);
        transform.position = v;
	}
}
