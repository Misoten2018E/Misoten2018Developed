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
		RightHand,
		Body,
		Tale
	}

	public bool TypeCheck(BodyType type) {
		return (bodyType == type);
	}

	public void Activate() {

		gameObject.SetActive(true);
	}


	public void Disable() {

		gameObject.SetActive(false);
	}

	//========================================================================================
	//                                    override
	//========================================================================================


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
