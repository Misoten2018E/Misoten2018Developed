using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectBlowOff : HitObject {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float BlowOffLength = 1f;

	//========================================================================================
	//                                    public - override
	//========================================================================================


	// Use this for initialization
	void Awake() {
		hitType = HitType.BlowOff;
	}

	public void BlowOff(DamagedAction action, Vector3 impact) {
		action.KnockBack(impact, Moved, MoveCurve);
		PlaySE();
	}

	//========================================================================================
	//                                    public
	//========================================================================================

	public float Moved {
		get { return BlowOffLength; }
	}
}
