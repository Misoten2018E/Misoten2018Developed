using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyFixedPop : TimelineEventStandard {

	[SerializeField] private EnemyData[] Enemy;
	[SerializeField] private float PopInterval = 1f;

	private void Awake() {
		isActive = false;
		EventIndex = 0;
	}

	public override void EventStart() {

		isActive = true;
		EventIndex = 0;
		CreateEnemy();
	}

	void CreateEnemy() {

		var enemy = Instantiate(Enemy[EventIndex].Enemy);
		UsedInitData eneData = new UsedInitData();
		eneData.BasePosition = this.transform;
		enemy.InitEnemy(eneData);
	}

	void TimeUpdate() {


		ElapsedTime += Time.deltaTime;
		if (PopInterval < ElapsedTime) {

			ElapsedTime -= PopInterval;
			CreateEnemy();
			EventIndex++;

			if (EventIndex >= Enemy.Length) {
				isActive = false;
			}
		}
	}

	private void Update() {

		if (!isActive) {
			return;
		}

		TimeUpdate();
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	// 経過時間
	float ElapsedTime;

	// 経過イベント数
	int EventIndex;

	// 起動中
	bool _isActive;
	public bool isActive {
		private set { _isActive = value; }
		get { return _isActive; }
	}
      


	[System.Serializable]
	public class EnemyData {

		[SerializeField] public MoveFixedEnemy Enemy;
		[SerializeField] public Transform Route1;
		[SerializeField] public Transform Route2;

	}
}
