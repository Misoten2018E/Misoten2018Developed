using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var c = CameraManager.Instance.FocusCamera;
		var can = GetComponent<Canvas>();
		can.worldCamera = c.MainCamera;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
