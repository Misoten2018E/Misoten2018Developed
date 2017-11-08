using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMiniAnimation : ControlAnimatorEnemy {


	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {

		NowType = AnimationType.Move;
	}


	//========================================================================================
	//                                    public
	//========================================================================================

	
	public void Animation(AnimationType type) {

		//　状態変更
		SetState((int)type);

		//switch (type) {
		//	case AnimationType.Move:
		//		AnimEnd = null;
		//		break;
		//	case AnimationType.AttackPose:
		//		AnimEnd = AttackAnimationPoseEnd;
		//		break;
		//	case AnimationType.Attack:
		//		AnimEnd = AttackAnimationEnd;
		//		break;
		//	case AnimationType.CityPose:
		//		AnimEnd = null;
		//		break;
		//	case AnimationType.RunAway:
		//		AnimEnd = null;
		//		break;
		//	default:
		//		break;
		//}

		NowType = type;
	}

	public void DamageAnimation() {

		base.Damaged();
		AnimEnd = DamageEnd;
	}

	/// <summary>
	/// アニメーション終了時コールバック
	/// </summary>
	/// <param name="callback"></param>
	public void SetAnimationEndCallback(System.Action callback) {

		AnimEnd = callback;
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	public enum AnimationType {
		Move,
		AttackPose,
		Attack,
		CityPose,
		RunAway
	}

	AnimationType _nowType;
	public AnimationType NowType {
		private set { _nowType = value; }
		get { return _nowType; }
	}

	//System.Action AnimEndCallback;

	/// <summary>
	/// 攻撃開始モーション終了
	/// </summary>
	//void AttackAnimationPoseEnd() {

	//	if (AnimEndCallback != null) {
	//		AnimEndCallback();
	//		AnimEndCallback = null;
	//	}
	//}

	///// <summary>
	///// 攻撃モーション終了
	///// </summary>
	//void AttackAnimationEnd() {
	//	if (AnimEndCallback != null) {
	//		AnimEndCallback();
	//		AnimEndCallback = null;
	//	}
	//}

	///// <summary>
	///// ダメージモーション終了
	///// </summary>
	//void DamageAnimationEnd() {

	//	if (AnimEndCallback != null) {
	//		AnimEndCallback();
	//		AnimEndCallback = null;
	//	}
	//}
}
