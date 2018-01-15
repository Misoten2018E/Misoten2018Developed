using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfoAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var a = Anim;
	}

	const string Trigger = "isTrigger";
	const string State = "State";

	public void StartAnimation() {

		gameObject.SetActive(true);
		AnimatorStateInfo stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
		Anim.Play(stateInfo.fullPathHash, 0, 0.0f);  //初期位置に戻す
	}

	public void EndAnimation() {
	//	Anim.GetCurrentAnimatorStateInfo
	////	Anim.SetInteger(State, 1);
	//	Anim.SetTrigger(Trigger);
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
      
}
