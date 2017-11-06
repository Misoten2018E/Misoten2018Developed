using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MultiInput))]
public class IntroPlayer : MonoBehaviour {



	//========================================================================================
	//                                    public
	//========================================================================================

	public void CheckStart(CheckPlayers plMng) {

		enabled = true;
		checker = plMng;
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

		if (isReadyOK) {
			return;
		}

		if (multiInput.GetButtonCircleTrigger()) {
			isReadyOK = true;
			StartCoroutine(IEReadyEvent());
		}
	}



	//========================================================================================
	//                                    private
	//========================================================================================

	MultiInput multiInput;

	CheckPlayers checker;

	bool _isReadyOK;
	public bool isReadyOK {
		private set { _isReadyOK = value; }
		get { return _isReadyOK; }
	}
     
	IEnumerator IEReadyEvent() {

		yield return null;

		print("Player" + multiInput.PlayerNo + "OK");
		checker.CheckReady();
	}
}
