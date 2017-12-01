using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyMediumPop : EventEnemyTimePop<EventEnemyMediumPop.EnemyData> {

	/// <summary>
	/// 敵作成
	/// </summary>
	override protected void CreateEnemy() {

		var enemy = Instantiate(Enemy[EventIndex].Enemy);
		UsedInitData eneData = new UsedInitData();
		eneData.BasePosition = this.transform;
		enemy.InitEnemy(eneData);

		enemy.GroupFormRate = Enemy[EventIndex].GroupFormationRate;
		PopEnemyList.Add(enemy);
	}


	[System.Serializable]
	public class EnemyData {

		[SerializeField] public MediumEnemy Enemy;
		[Range(0,100)]
		[SerializeField] public int GroupFormationRate;
	}
}
