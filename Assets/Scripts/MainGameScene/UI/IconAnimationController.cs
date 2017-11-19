using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IconAnimationController : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	public void StartAnimSet() {
		Anim.SetTrigger(TrigSet);
	}

	public void StartAnimRemove() {
		Anim.SetTrigger(TrigRemove);
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	// Use this for initialization
	void Start() {

		var c = Anim;
	}

	//========================================================================================
	//                                     private
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

	private const string TrigSet = "IsSet";
	private const string TrigRemove = "IsRemove";
}
