using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupLeaderEnemyCommand : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	public void GroupInitialize() {

		var cp = GetComponentsInChildren<GroupEnemyCheckPoint>();
		PointList.AddRange(cp);
	}

	public void CollectMembers() {

		var move = EnemyManager.Instance.GetEnemyList(EnemyTypeBase.EnemyType.MoveFixed);
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================

	List<GroupEnemyCheckPoint> PointList = new List<GroupEnemyCheckPoint>();
}


public interface IFGroupEnemyCommand {

	void GroupStart(Transform target);

	void GroupAttack();

	void GroupEnd();

}