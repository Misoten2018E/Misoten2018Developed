using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopEnemy : AimingPlayerEnemy {

	protected override void Start() {

		StopMove(5f);
		ieAttackMode = PopedMotion();
		StartCoroutine(ieAttackMode);
	}

	public override void InitEnemy(UsedInitData InitData) {

		MyType = EnemyType.BossPop;
		EnemyManager.Instance.SetEnemy(this);
	}


	IEnumerator PopedMotion() {

		yield return new WaitForSeconds(2f);
		base.Start();
	}
	
}
