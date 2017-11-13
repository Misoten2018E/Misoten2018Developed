using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDebugObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	float time = 0f;


	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Space)) {
			ResultManager.Instance.NextSceneStart();
		}
	}
}
