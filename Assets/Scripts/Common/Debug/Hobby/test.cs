using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	float time = 0;
	bool isEnd = false;

	// Update is called once per frame
	void Update() {

		time += Time.deltaTime;

		if (time >= 3.0f && (isEnd == false)) {
			var tex = CameraCapture.Capture(Camera.main);
			meshRen.material.mainTexture = tex;
			//isEnd = true;
			time = 0.0f;
		}

	}


	MeshRenderer _meshRen;
	public MeshRenderer meshRen {
		get {
			if (_meshRen == null) {
				_meshRen = GetComponent<MeshRenderer>();
			}
			return _meshRen;
		}
	}

}
