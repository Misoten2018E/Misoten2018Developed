using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotographCamera : MonoBehaviour {

	Camera _MyCamera;
	public Camera MyCamera {
		private set { _MyCamera = value; }
		get {
			if (_MyCamera == null) {
				MyCamera = GetComponent<Camera>();
			}
			return _MyCamera;
		}
	}
      

	// Use this for initialization
	void Start() {
		MyCamera = GetComponent<Camera>();
		gameObject.SetActive(false);
	}
	

	public Texture2D Photo(int width,int height) {

		gameObject.SetActive(true);
		var tex = CameraCapture.Capture(MyCamera, width, height);
		DebugLog.log("キャプチャ");
		gameObject.SetActive(false);
		return tex;
	}
}
