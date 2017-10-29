﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	List<IFIntroStartEvent> IntroStartEvent;

	//========================================================================================
	//                                    public
	//========================================================================================

	// シングルトンインスタンス
	static IntroductManager myInstance;
	static public IntroductManager Instance {
		get {
			return myInstance;
		}
	}


	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//========================================================================================
	//                                    private
	//========================================================================================


}


public interface IFIntroStartEvent {

	void StartEvent();
}
