using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMediumAnimation : ControlAnimatorEnemy {


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

		transform.localRotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;

		switch (type) {
			case AnimationType.Move:
				SetState((int)AnimationType.Move);
				AnimEnd = null;
				break;

			case AnimationType.AttackPose:
				AttackPose();
				break;
			case AnimationType.Attack1:
				SetState((int)AnimationType.Attack1);
				Attack();
				break;
			case AnimationType.Attack2:
				SetState((int)AnimationType.Attack2);
				Attack();
				break;
			case AnimationType.Attack3:
				SetState((int)AnimationType.Attack3);
				Attack();
				break;
			case AnimationType.RunAway:
				SetState((int)AnimationType.RunAway);
				AnimEnd = null;
				break;
			case AnimationType.Damage:
				AnimEnd = AnimationDamageEnd;
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
		Attack1,
		Attack2,
		Attack3,
		RunAway,
		Damage,
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
