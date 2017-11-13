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

		switch (type) {
			case AnimationType.Move:
				SetState((int)AnimationType.Move);
				AnimEnd = null;
				break;

			case AnimationType.AttackPose:
				AttackPose();
				break;
			case AnimationType.Attack:
				Attack();
				break;
			case AnimationType.Damage:
				AnimEnd = AnimationDamageEnd;
				break;
			case AnimationType.CityPose:
				AnimEnd = null;
				break;
			case AnimationType.RunAway:
				AnimEnd = null;
				break;
			default:
				break;
		}

		NowType = type;
	}

	private void AnimationDamageEnd() {

		SetState((int)AnimationType.Move);
		base.DamageEnd();
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
		Damage,
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
