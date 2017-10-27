using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RelayPoint : MonoBehaviour,DebuggableObject {

	public void Debug(bool isDebugMode) {

		var m = GetComponent<MeshRenderer>();
		m.enabled = isDebugMode;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
