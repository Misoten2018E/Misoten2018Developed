using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerAttackEnemy : MoveTargetEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float PlayerCheckPeriod;
	[SerializeField] private float AttackRange;
	[SerializeField] private float AttackAngle;

	[SerializeField] private CheckEnemyAttackArea AttackCheckArea;

	//========================================================================================
	//                                    public- override
	//========================================================================================

	public override void InitEnemy(UsedInitData InitData) {
		AttackCheckArea.gameObject.SetActive(false);
	}


	//========================================================================================
	//                                    protected
	//========================================================================================

	/// <summary>
	/// 攻撃判定エリアチェック開始
	/// 最も近くのプレイヤーの情報も保持するのでアクセス可能
	/// </summary>
	protected void StartPlayerAttackMode() {

		iePlCheck = PlayerCheck();
		StartPlayerAttackMode(GetNearPlayer());
		StartCoroutine(iePlCheck);
		//	AttackCheckArea.gameObject.SetActive(true);
		//	AttackCheckArea.SetHitEnterCallback(ChangeAttack);
	}

	/// <summary>
	/// 攻撃判定エリアチェック開始
	/// ターゲットはセットしたものを狙い続ける
	/// </summary>
	protected void StartPlayerAttackMode(PlayerBase player) {

		TargetPlayer = player;
		IsAttackMode = true;
	}

	/// <summary>
	/// 攻撃判定エリアチェック終了
	/// </summary>
	protected void StopPlayerAttackMode() {

		if (iePlCheck != null) {
			StopCoroutine(iePlCheck);
		}
		
		TargetPlayer = null;
		//	AttackCheckArea.SetHitEnterCallback(null);
		//	AttackCheckArea.gameObject.SetActive(false);
		IsAttackMode = false;
	}

	/// <summary>
	/// 距離で攻撃するかを決める
	/// 代替案
	/// </summary>
	protected void ChangeAttackToRange() {

		if (TargetPlayer == null) {
			return;
		}

		Vector3 diff = transform.position - TargetPlayer.transform.position;

		// 距離判定
		if (CheckAttackRange(diff)) {

			// 角度判定
			if (CheckAttackAngle(diff)) {

				if (AttackAction != null) {
					AttackAction();
					AttackAction = null;
				}
			}
		}
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	IEnumerator iePlCheck;


	System.Action _AttackAction;
	protected System.Action AttackAction {
		set { _AttackAction = value; }
		private get { return _AttackAction; }
	}

	bool _isAttackMode;
	/// <summary>
	/// 攻撃モードなら
	/// </summary>
	public bool IsAttackMode {
		protected set { _isAttackMode = value; }
		get { return _isAttackMode; }
	}


	bool _isAttacking;
	/// <summary>
	/// 攻撃中なら
	/// <para>管理は子クラスに任せる</para>
	/// <para>暫定的な措置</para>
	/// </summary>
	public bool IsAttacking {
		protected set { _isAttacking = value; }
		get { return _isAttacking; }
	}


	PlayerBase _TargetPlayer;
	/// <summary>
	/// ターゲットされたプレイヤー
	/// nullの可能性アリ
	/// </summary>
	public PlayerBase TargetPlayer {
		protected set { _TargetPlayer = value; }
		get { return _TargetPlayer; }
	}


	/// <summary>
	/// 一定間隔毎にプレイヤーの位置状況のチェック
	/// </summary>
	/// <returns></returns>
	IEnumerator PlayerCheck() {

		while (true) {

			yield return new WaitForSeconds(PlayerCheckPeriod);

			var pl = GetNearPlayer();
			if (pl != TargetPlayer) {
				TargetPlayer = pl;
			}
		}
	}

	/// <summary>
	/// 近くのプレイヤーサーチ
	/// </summary>
	/// <returns></returns>
	PlayerBase GetNearPlayer() {

		var pls = GameObject.FindObjectsOfType<PlayerBase>();

		float length = 99999f;
		int id = -1;

		for (int i = 0; i < pls.Length; i++) {

			float mag = pls[i].transform.position.magnitude;

			if (length > mag) {
				length = mag;
				id = i;
			}
		}

		return pls[id];
	}

	/// <summary>
	/// 攻撃開始
	/// </summary>
	/// <param name="col"></param>
	private void ChangeAttack(Collider col) {

		if (col.CompareTag(ConstTags.Player)) {
			print("hit player" + gameObject.name);

			if (AttackAction != null) {
				AttackAction();
				AttackAction = null;
			}
		}
	}



	/// <summary>
	/// 正面にいるかどうかのチェック
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	private bool CheckAttackAngle(Transform target) {

		return CheckAttackRange(transform.position - target.transform.position);
	}

	/// <summary>
	/// 正面にいるかどうかのチェック
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	private bool CheckAttackAngle(Vector3 diffPosition) {

		Vector3 dir = diffPosition.normalized;
		float dot = Vector3.Dot(transform.forward, dir);

		float deg = Mathf.Rad2Deg * (Mathf.Acos(dot));

		return (deg <= AttackAngle);
	}

	/// <summary>
	/// 攻撃範囲のチェック
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	private bool CheckAttackRange(Transform target) {

		return CheckAttackRange(transform.position - target.transform.position);
	}

	/// <summary>
	/// 攻撃範囲のチェック
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	private bool CheckAttackRange(Vector3 diffPosition) {

		float range = diffPosition.magnitude;
		return (range <= AttackRange);
	}
}
