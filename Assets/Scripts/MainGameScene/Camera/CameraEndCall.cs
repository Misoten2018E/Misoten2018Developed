using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEndCall : MonoBehaviour {

	public void AnimationEnd() {

		if (EndCallBack != null) {
			EndCallBack();
			EndCallBack = null;
		}
	}


	System.Action _EndCallBack;
	public System.Action EndCallBack {
		set { _EndCallBack = value; }
		get { return _EndCallBack; }
	}



	Animator _MyAnim;
	public Animator MyAnim {
		get {
			if (_MyAnim == null) {
				_MyAnim = GetComponent<Animator>();
			}
			return _MyAnim;
		}
	}
      
}
