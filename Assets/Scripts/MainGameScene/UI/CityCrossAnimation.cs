using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCrossAnimation : MonoBehaviour {



	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private Transform CityTransform;

	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start() {

		MainCamera = CameraManager.Instance.FocusCamera.MainCamera;
	}

	// Update is called once per frame
	void Update() {
		
		var pos = MainCamera.WorldToViewportPoint(CityTransform.position);
		rectTrs.position = MainCamera.ViewportToScreenPoint(pos);
	}



	Camera _MainCamera;
	public Camera MainCamera {
		private set { _MainCamera = value; }
		get { return _MainCamera; }
	}


	RectTransform _rectTrs;
	public RectTransform rectTrs {
		get {
			if (_rectTrs == null) {
				_rectTrs = GetComponent<RectTransform>();
			}
			return _rectTrs;
		}
	}
      
}
