using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private FocusCamera focusCamera;

	[SerializeField] private AnotherScreenFocusCamera AnotherCamera;

	[SerializeField] private PhotographCamera PhotographCamera;


	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake () {

		myInstance = this;
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

	public FocusCamera FocusCamera {
		get { return focusCamera; }
	}

	//========================================================================================
	//                                    private
	//========================================================================================



}
