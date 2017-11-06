using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
[System.Serializable]
public abstract class HitObject : MonoBehaviour,DebuggableObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private int AttackId = 0;

	[SerializeField] private int Damage = 100;

	[SerializeField] AnimationCurve _MoveCurve;

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

	public enum HitType{
		Impact,		// 衝撃
		BlowOff,	// 吹き飛ばし
		Suction		// 吸引
	}

	public void Initialize(HitSeriesofAction parent) {
		ParentHit = parent;

#if UNITY_DEBUG
		
#else
		var m = GetComponent<MeshRenderer>();
		m.enabled = false;
#endif

	}

	

	public void Debug(bool isDebugMode) {

		var m = GetComponent<MeshRenderer>();
		m.enabled = isDebugMode;
	}

	public virtual void DamageHp(ObjectHp hp) {
		hp.Damage(Damage);
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

	public int Id {
		get { return AttackId; }
	}

	public AnimationCurve MoveCurve {
		get { return _MoveCurve; }
	}


	//float _NowTime;
	//public float NowTime {
	//	private set { _NowTime = value; }
	//	get { return _NowTime; }
	//}

	HitSeriesofAction _ParentHit;
	public HitSeriesofAction ParentHit {
		protected set { _ParentHit = value; }
		get { return _ParentHit; }
	}



	HitType _HitType;
	public HitType hitType {
		protected set { _HitType = value; }
		get { return _HitType; }
	}
}

