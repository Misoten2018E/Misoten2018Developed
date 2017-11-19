using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDebugObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		isEnd = false;
	}

	float time = 0f;
	bool isEnd = false;

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space) && (!isEnd)) {
			isEnd = true;
			ResultManager.Instance.NextSceneStart();
		}
	}
}
