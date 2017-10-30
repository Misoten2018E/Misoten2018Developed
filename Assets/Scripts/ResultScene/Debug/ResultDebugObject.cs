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

		time += Time.deltaTime;
		if (time > 5f) {
			ResultManager.Instance.NextSceneStart();
			time = -50f;
		}
	}
}
