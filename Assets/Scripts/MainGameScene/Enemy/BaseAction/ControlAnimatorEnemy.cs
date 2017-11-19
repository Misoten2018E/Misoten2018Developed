using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControlAnimatorEnemy : MonoBehaviour {


	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// アニメーション終了時コールバックを呼ぶ
	/// </summary>
	public void AnimationEnd() {

		if (AnimEnd != null) {
			AnimEnd();
			AnimEnd = null;
		}
	}

	/// <summary>
	/// 更新
	/// </summary>
	private void Update() {

		if (CheckAnimationEnd(OldFrameTime, out OldFrameTime)) {
			AnimationEnd();
		}
	}

	//========================================================================================
	//                                    protected
	//========================================================================================


	Animator _Anim;
	protected Animator Anim {
		set { _Anim = value; }
		get {
			if (_Anim == null) {
				_Anim = GetComponent<Animator>();
			}
			return _Anim;
		}
	}

	/// <summary>
	/// ステート変更
	/// </summary>
	/// <param name="num"></param>
	protected void SetState(int num) {

		Anim.SetInteger(StrState, num);
	}

	/// <summary>
	/// ダメージを喰らう
	/// </summary>
	protected void Damaged() {

		Anim.SetTrigger(StrIsDown);
	}

	/// <summary>
	/// ダメージを喰らい終える
	/// </summary>
	protected void DamageEnd() {

		Anim.SetTrigger(StrIsDownEnd);
	}

	protected void AttackPose() {

		Anim.SetTrigger(StrIsAttackPose);

	}

	protected void Attack() {

		Anim.SetTrigger(StrIsAttack);

	}


	System.Action _AnimEndCallBack;
	/// <summary>
	/// アニメーション終了時に呼ばれる
	/// </summary>
	protected System.Action AnimEnd {
		set { _AnimEndCallBack = value; }
		get { return _AnimEndCallBack; }
	}
      

	//========================================================================================
	//                                    private
	//========================================================================================

	const string StrState = "State";
	const string StrIsDown = "isDown";
	const string StrIsDownEnd = "isDownEnd";
	const string StrIsAttackPose = "isAttackPose";
	const string StrIsAttack = "isAttack";

	private float OldFrameTime;

	/// <summary>
	/// アニメーションの区切りが来ていたらtrue
	/// </summary>
	/// <returns></returns>
	private bool CheckAnimationEnd(float oldTime ,out float setNowTime) {

		var a = Anim.GetCurrentAnimatorStateInfo(0);
		float nowTime = a.normalizedTime;
		setNowTime = nowTime;

		return (nowTime >= 1.0f || (oldTime >= nowTime));
	}
}
