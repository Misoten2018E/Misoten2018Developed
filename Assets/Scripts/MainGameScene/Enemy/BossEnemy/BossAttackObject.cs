using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitObject))]
public class BossAttackObject : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private BodyType bodyType;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum BodyType {
		LeftLeg,
		RightLeg,
		Body,
		Tale
	}

	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start() {

	}


	//========================================================================================
	//                                     private
	//========================================================================================


	HitObject _HitObj;
	public HitObject HitObj {
		get {
			if (_HitObj == null) {
				_HitObj = GetComponent<HitObject>();
			}
			return _HitObj;
		}
	}
}
