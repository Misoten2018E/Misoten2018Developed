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

	public void GroupInitialize() {

		var cp = GetComponentsInChildren<GroupEnemyCheckPoint>();
		PointList.AddRange(cp);
	}

	public void CollectMembers() {

		var move = EnemyManager.Instance.GetEnemyList(EnemyTypeBase.EnemyType.MoveFixed);

		float len2 = CorrectLength * CorrectLength;

		MoveFixedEnemy[] list = new MoveFixedEnemy[GroupMemberNum];
		int id = 0;

		for (int i = 0; i < move.Count; i++) {

			float diff = (move[i].transform.position - transform.position).sqrMagnitude;

			if (len2 >= diff) {
				list[id] = move[i] as MoveFixedEnemy;
				id++;

				if (id >= GroupMemberNum) {
					break;
				}
			}
		}


	}

	//========================================================================================
	//                                 public - override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================

	List<GroupEnemyCheckPoint> PointList = new List<GroupEnemyCheckPoint>();

	const int GroupMemberNum = 6;
}


public interface IFGroupEnemyCommand {

	void GroupStart(Transform target);

	void GroupAttack();

	void GroupEnd();

}