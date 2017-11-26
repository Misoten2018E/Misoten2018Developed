using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyFixedPop : EventEnemyTimePop<EventEnemyFixedPop.EnemyData> {

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

		enemy.AddTarget(Enemy[EventIndex].Route1, true);
		enemy.AddTarget(Enemy[EventIndex].Route2);

		PopEnemyList.Add(enemy);
	}

	[System.Serializable]
	public class EnemyData {

		[SerializeField] public MoveFixedEnemy Enemy;
		[SerializeField] public Transform Route1;
		[SerializeField] public Transform Route2;

	}
}
