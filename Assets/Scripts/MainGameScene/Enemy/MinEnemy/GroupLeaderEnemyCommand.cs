using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupLeaderEnemyCommand : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float CorrectLength = 5f;

	//========================================================================================
	//                                     public
	//========================================================================================

	/// <summary>
	/// 初期処理
	/// </summary>
	public void GroupInitialize() {

		var cp = GetComponentsInChildren<GroupEnemyCheckPoint>();
		PointList.AddRange(cp);
	}

	/// <summary>
	/// 集合を知らせる
	/// </summary>
	/// <param name="enemyList"></param>
	public void NoticeEnemiesGather() {

		CollectMembers();

		for (int i = 0; i < EnemyList.Count; i++) {

			if (EnemyList[i] != null) {
				EnemyList[i].GroupStart(PointList[i].transform);
			}
		}
	}

	/// <summary>
	/// 攻撃を知らせる
	/// </summary>
	/// <param name="enemyList"></param>
	public void NoticeEnemiesAttack() {

		for (int i = 0; i < EnemyList.Count; i++) {
			if (EnemyList[i] != null) {
				EnemyList[i].GroupAttack();
			}
		}
	}

	/// <summary>
	/// グループ行動終了を知らせる
	/// </summary>
	/// <param name="enemyList"></param>
	public void NoticeEnemiesGroupEnd() {

		for (int i = 0; i < EnemyList.Count; i++) {
			if (EnemyList[i] != null) {
				EnemyList[i].GroupEnd();
			}
		}
		EnemyList.Clear();
	}


	List<MoveFixedEnemy> _EnemyList = new List<MoveFixedEnemy>();
	public List<MoveFixedEnemy> EnemyList {
		private set { _EnemyList = value; }
		get { return _EnemyList; }
	}
      

	//========================================================================================
	//                                 public - override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================

	List<GroupEnemyCheckPoint> PointList = new List<GroupEnemyCheckPoint>();

	const int GroupMemberNum = 6;

	/// <summary>
	/// メンバーを集める
	/// </summary>
	/// <param name="enemyList"></param>
	void CollectMembers() {

		var move = EnemyManager.Instance.GetEnemyList(EnemyTypeBase.EnemyType.MoveFixed);

		float len2 = CorrectLength * CorrectLength;
		int id = 0;

		for (int i = 0; i < move.Count; i++) {

			var enemy = move[i] as MoveFixedEnemy;
			float diff = (enemy.transform.position - transform.position).sqrMagnitude;

			if (len2 >= diff && (!enemy.IsEscape) && (!enemy.IsGroupMode)) {
				EnemyList.Add(enemy);
				id++;

				if (id >= GroupMemberNum) {
					break;
				}
			}
		}
	}
}


public interface IFGroupEnemyCommand {

	void GroupStart(Transform target);

	void GroupAttack();

	void GroupEnd();

	bool IsGroupMode {
		get;
	}
}