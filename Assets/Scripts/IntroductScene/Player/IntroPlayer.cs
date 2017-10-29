using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MultiInput))]
public class IntroPlayer : MonoBehaviour {



	//========================================================================================
	//                                    public
	//========================================================================================

	public void CheckStart() {

		enabled = true;

	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake () {
		multiInput = GetComponent<MultiInput>();

		isReadyOK = false;

		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (multiInput.GetButtonCircleTrigger()) {
			isReadyOK = true;
		}
	}



	//========================================================================================
	//                                    private
	//========================================================================================

	MultiInput multiInput;

	bool _isReadyOK;
	public bool isReadyOK {
		private set { _isReadyOK = value; }
		get { return _isReadyOK; }
	}
      
}
