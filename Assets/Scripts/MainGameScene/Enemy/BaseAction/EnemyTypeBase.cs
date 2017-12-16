using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HittedDamage),typeof(ObjectHp))]
[DisallowMultipleComponent]
public abstract class EnemyTypeBase : PauseSupport {

	public enum EnemyType {
		MoveFixed ,
		PlayerAttack,
		Middle,
		BossPop,
		Boss
	}

	public const int EnemyTypeMax = (int)EnemyType.Boss + 1;

	EnemyType _MyType;
	public EnemyType MyType {
		protected set { _MyType = value; }
		get { return _MyType; }
	}      

	/// <summary>
	/// 敵初期化
	/// </summary>
	public abstract void InitEnemy(UsedInitData InitData);

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

	public abstract bool IsDeath {
		protected set;
		get;
	}
}

public struct UsedInitData {
	public Transform BasePosition;
}
