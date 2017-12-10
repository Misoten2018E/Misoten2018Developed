using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyAimingPlayerPop : EventEnemyTimePop<EventEnemyAimingPlayerPop.EnemyData> {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                    public
	//========================================================================================

	//========================================================================================
	//                                    public - override
	//========================================================================================

	protected override void Awake() {
		base.Awake();
	}

	protected override void Update() {
		base.Update();
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	/// <summary>
	/// 敵作成
	/// </summary>
	override protected void CreateEnemy() {

		var enemy = Instantiate(Enemy[EventIndex].Enemy);
		UsedInitData eneData = new UsedInitData();
		eneData.BasePosition = this.transform;
		enemy.InitEnemy(eneData);

		PopEnemyList.Add(enemy);
	}

	[System.Serializable]
	public class EnemyData {

		[SerializeField] public AimingPlayerEnemy Enemy;
	}
}
