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
	void Start () {
		hitType = HitType.Freeze;
	}

	public void Freeze(DamagedAction action) {
		action.Freeze(FreezeTime);
	}

	//========================================================================================
	//                                    public
	//========================================================================================

	public float FreezeTime {
		get { return _FreezeTime; }
	}
}
