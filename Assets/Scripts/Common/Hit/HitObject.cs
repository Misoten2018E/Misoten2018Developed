﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class HitObject : MonoBehaviour,DebuggableObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	//[SerializeField] private float AppearanceTime = 0.3f;

	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// 当たり判定開始
	/// </summary>
	//public virtual void Activate() {

	//	ReStart();
	//}

	/// <summary>
	/// 再度開始
	/// </summary>
	//public virtual void ReStart() {

	//	gameObject.SetActive(true);
	//	collider.enabled = true;
	//	NowTime = 0f;
	//}

	///// <summary>
	///// 終了(基本外からは呼ばない)
	///// </summary>
	//public virtual void Finish() {

	//	gameObject.SetActive(false);
	//	collider.enabled = false;
	//	NowTime = 0f;
	//}

	//// Update is called once per frame
	//virtual protected void Update () {

	//	NowTime += Time.deltaTime;
	//	if (NowTime >= AppearanceTime) {
	//		Finish();
	//	}
	//}

	public void Debug(bool isDebugMode) {

		var m = GetComponent<MeshRenderer>();
		m.enabled = isDebugMode;
	}


	//========================================================================================
	//                                    propety
	//========================================================================================


	Collider _collider;
	public Collider collider {
		get {
			if (_collider == null) {
				_collider = GetComponent<Collider>();
			}
			return _collider;
		}
	}


	float _NowTime;
	public float NowTime {
		private set { _NowTime = value; }
		get { return _NowTime; }
	}
      
}
