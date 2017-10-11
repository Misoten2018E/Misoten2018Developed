using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class HitSeriesofAction : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================



	//========================================================================================
	//                                    const
	//========================================================================================

	const string ANIM_ACCESS_STATUS = "isActivate";

	//========================================================================================
	//                                    public
	//========================================================================================

	// Use this for initialization
	void Start () {

		var a = Anim;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate() {

		Anim.SetBool(ANIM_ACCESS_STATUS, true);
	}


	//========================================================================================
	//                                    propety
	//========================================================================================

	Animator _Anim;
	public Animator Anim {
		get {
			if (_Anim == null) {
				_Anim = GetComponent<Animator>();
			}
			return _Anim;
		}
	}
      
}
