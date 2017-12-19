using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectImpact : HitObject {
	


	//========================================================================================
	//                                    inspector
	//========================================================================================

	

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake () {
		hitType = HitType.Impact;
	}

	public void Impact(DamagedAction action ,Vector3 impact) {
		action.KnockBack(impact, 0.1f, MoveCurve);
		PlaySE();
	}

	//========================================================================================
	//                                    private
	//========================================================================================
}
