using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private AnotherScreenFocusCamera AnotherCamera;

	[SerializeField] private PhotographCamera PhotographCamera;


	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake () {

		myInstance = this;
		FocusCamera = GetComponent<FocusCamera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//========================================================================================
	//                                    public
	//========================================================================================


	// シングルトンインスタンス
	static CameraManager myInstance;
	static public CameraManager Instance {
		get {
			return myInstance;
		}
	}

	public AnotherScreenFocusCamera SubCamera {
		get { return AnotherCamera; }
	}
    
	public PhotographCamera PhotoCamera {
		get { return PhotographCamera; }
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	FocusCamera _FocusCamera;
	public FocusCamera FocusCamera {
		private set { _FocusCamera = value; }
		get { return _FocusCamera; }
	}
      
}
