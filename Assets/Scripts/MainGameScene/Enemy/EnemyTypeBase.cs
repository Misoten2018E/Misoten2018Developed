using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HittedDamage),typeof(ObjectHp))]
public abstract class EnemyTypeBase : PauseSupport {

	/// <summary>
	/// 敵初期化
	/// </summary>
	public abstract void InitEnemy(UsedInitData InitData);

	

	/// <summary>
	/// 目標に向かって進む
	/// </summary>
	/// <param name="target"></param>
	/// <param name="speed"></param>
	protected void MoveAdvanceToTarget(Transform target, float speed, float angle = 0.03f) {

		RotateToTarget(target, angle);
		MoveForward(speed);
	}


	/// <summary>
	/// 目標への回転
	/// </summary>
	/// <param name="target"></param>
	/// <param name="angle"></param>
	protected void RotateToTarget(Transform target, float angle) {

		var endQt = Quaternion.LookRotation(target.position - transform.position);

		transform.rotation = Quaternion.Slerp(transform.rotation, endQt, (angle));
	}

	protected void MoveForward(float speed) {

		Vector3 v = transform.forward;
		v.y = 0f;
		v.Normalize();
		transform.position += v * speed * Time.deltaTime;

	}



	HittedDamage _Damaged;
	public HittedDamage Damaged {
		protected set { _Damaged = value; }
		get {
			if (_Damaged == null) {
				_Damaged = GetComponent<HittedDamage>();
			}
			return _Damaged;
		}
	}

	ObjectHp _MyHp;
	public ObjectHp MyHp {
		protected set { _MyHp = value; }
		get {
			if (_MyHp == null) {
				_MyHp = GetComponent<ObjectHp>();
			}
			return _MyHp;
		}
	}

	HitLogList _HitLog = new HitLogList();
	public HitLogList HitLog {
		private set { _HitLog = value; }
		get { return _HitLog; }
	}
}

public struct UsedInitData {
	public Transform BasePosition;
}
