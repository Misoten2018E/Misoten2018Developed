using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
[System.Serializable]
public abstract class HitObject : SoundEffectSupport,DebuggableObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private int AttackId = 0;

	[SerializeField] private int _Damage = 100;

	[SerializeField] AnimationCurve _MoveCurve;

	[SerializeField] HittedEffect EffectPattern;

	//========================================================================================
	//                                    public
	//========================================================================================

	public enum HitType{
		Impact,		// 衝撃
		BlowOff,	// 吹き飛ばし
		Suction,	// 吸引
		Freeze,		// 動きを止める
	}

	public void Initialize(HitSeriesofAction parent) {
		ParentHit = parent;

#if UNITY_DEBUG

#else
		Debug(false);
#endif

	}



	public void Debug(bool isDebugMode) {

		var m = GetComponent<MeshRenderer>();
		m.enabled = isDebugMode;
	}

	public virtual void DamageHp(ObjectHp hp) {
		hp.Damage(Damage);
	}

	/// <summary>
	/// ダメージ倍率変更
	/// </summary>
	/// <param name="rate"></param>
	public virtual void DamageUpMulti(float rate) {
		Damage = (int)(Damage * rate);
	}

	/// <summary>
	/// ダメージ追加
	/// </summary>
	/// <param name="plus"></param>
	public virtual void DamageUpPlus(int plus) {
		Damage += plus;
	}

	public virtual void HitEffect(Vector3 HittedPos ,Vector3 targetPos) {

		EffectPattern.CreateParticle(HittedPos, targetPos);
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

	public int Damage {
		private set { _Damage = value; }
		get { return _Damage; }
	}

	public int Id {
		get { return AttackId; }
	}

	public AnimationCurve MoveCurve {
		get { return _MoveCurve; }
	}

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

