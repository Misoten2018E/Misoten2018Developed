using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectFreeze : HitObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float _FreezeTime = 1f;

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake() {
		hitType = HitType.Freeze;
	}

	public void Freeze(DamagedAction action) {
		action.Freeze(FreezeTime);
		PlaySE();
	}

	//========================================================================================
	//                                    public
	//========================================================================================

	public float FreezeTime {
		get { return _FreezeTime; }
	}
}
