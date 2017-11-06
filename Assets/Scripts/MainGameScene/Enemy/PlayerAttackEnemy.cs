using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerAttackEnemy : MoveTargetEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float PlayerCheckPeriod;

	[SerializeField] private CheckEnemyAttackArea AttackCheckArea;

	//========================================================================================
	//                                    public- override
	//========================================================================================

	public override void InitEnemy(UsedInitData InitData) {
		AttackCheckArea.gameObject.SetActive(false);
	}

	/// <summary>
	/// 攻撃判定エリアチェック開始
	/// 最も近くのプレイヤーの情報も保持するのでアクセス可能
	/// </summary>
	protected void StartPlayerAttackMode() {

		iePlCheck = PlayerCheck();
		TargetPlayer = GetNearPlayer();
		StartCoroutine(iePlCheck);
		AttackCheckArea.gameObject.SetActive(true);
		AttackCheckArea.SetHitEnterCallback(ChangeAttack);
		isAttackMode = true;
	}

	/// <summary>
	/// 攻撃判定エリアチェック終了
	/// </summary>
	protected void StopPlayerAttackMode() {

		StopCoroutine(iePlCheck);
		TargetPlayer = null;
		AttackCheckArea.SetHitEnterCallback(null);
		AttackCheckArea.gameObject.SetActive(false);
		isAttackMode = false;
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
	public bool isAttackMode {
		protected set { _isAttackMode = value; }
		get { return _isAttackMode; }
	}
      

	Player _TargetPlayer;
	public Player TargetPlayer {
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
	Player GetNearPlayer() {

		var pls = GameObject.FindObjectsOfType<Player>();

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
}
