using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimationController : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum EnemyState {

		Wait,
		Jump,
		LeftTale,
		RightTale,
		Fire,
		EnemyPop,
		Howling,
		Move,
		Landed,
	}

	Animator _Anim;
	public Animator Anim {
		get {
			if (_Anim == null) {
				_Anim = GetComponent<Animator>();
			}
			return _Anim;
		}
	}

	/// <summary>
	/// 状態セット
	/// </summary>
	/// <param name="state"></param>
	public void SetState(EnemyState state) {

		Anim.SetInteger(StateString, (int)state);
		Anim.SetTrigger(ActionString);
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	// Use this for initialization
	void Start() {
		var a = Anim;
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	const string StateString = "NowState";
	const string ActionString = "Action";
	
}
