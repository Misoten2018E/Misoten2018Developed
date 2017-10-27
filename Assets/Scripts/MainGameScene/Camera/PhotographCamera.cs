using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotographCamera : MonoBehaviour {

	Camera _MyCamera;
	public Camera MyCamera {
		private set { _MyCamera = value; }
		get { return _MyCamera; }
	}
      

	// Use this for initialization
	void Start() {
		MyCamera = GetComponent<Camera>();
		gameObject.SetActive(false);
	}
	

	public Texture2D Photo(int width,int height) {

		return CameraCapture.Capture(MyCamera, width, height);

	}
}
