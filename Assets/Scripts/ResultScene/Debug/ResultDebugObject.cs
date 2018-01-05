using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDebugObject : MonoBehaviour {
    MultiInput Minput;

    // Use this for initialization
    void Start () {
		isEnd = false;
        Minput = GetComponent<MultiInput>(); ;
	}

	float time = 0f;
	bool isEnd = false;

	// Update is called once per frame
	void Update () {

		if (Minput.GetButtonCirclePress() && Minput.GetButtonSquarePress() && (!isEnd)) {
			isEnd = true;
			ResultManager.Instance.NextSceneStart();
		}
	}
}
