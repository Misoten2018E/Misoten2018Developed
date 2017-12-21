using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobChangeIconController : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================


	//========================================================================================
	//                                     public
	//========================================================================================


	public void AnimationStart() {

		MyAnim.SetBool(TriggerName, false);
		MyAnim.SetTrigger(StartName);
	}

	/// <summary>
	/// アニメーション終了
	/// </summary>
	public void AnimationEnd() {

		MyAnim.SetBool(TriggerName, true);
	}

	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start() {

		var a = MyAnim;
	}

	//========================================================================================
	//                                     private
	//========================================================================================


	const string TriggerName = "SelectedJob";
	const string StartName = "Start";



	Animator _MyAnim;
	private Animator MyAnim {
		get {
			if (_MyAnim == null) {
				_MyAnim = GetComponent<Animator>();
			}
			return _MyAnim;
		}
	}
      
}
