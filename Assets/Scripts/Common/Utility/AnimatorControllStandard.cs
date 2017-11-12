using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorControllStandard : MonoBehaviour {


	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start () {
		var a = Anim;
	}


	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// アニメーション開始
	/// </summary>
	public void AnimationStart() {

		gameObject.SetActive(true);
		AnimatorStateInfo stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
		Anim.Play(stateInfo.fullPathHash, 0, 0.0f);  //初期位置に戻す
	}

	/// <summary>
	/// アニメーション終了時に呼ばれる
	/// </summary>
	public void AnimationEnd() {

		if (AnimationEndCallback != null) {
			AnimationEndCallback();
			AnimationEndCallback = null;
		}
	}


	System.Action _AnimationEndCallback;
	public System.Action AnimationEndCallback {
		set { _AnimationEndCallback = value; }
		get { return _AnimationEndCallback; }
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
