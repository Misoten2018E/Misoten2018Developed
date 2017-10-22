using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyFixedPop : TimelineEventStandard {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private EnemyData[] Enemy;
	[SerializeField] private float PopInterval = 1f;



	//========================================================================================
	//                                    public
	//========================================================================================

	public void EventEnd() {

		isActive = false;
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	/// <summary>
	/// オブジェクト生成時
	/// </summary>
	private void Awake() {
		isActive = false;
		EventIndex = 0;
	}

	/// <summary>
	/// イベント開始
	/// </summary>
	public override void EventStart() {

		isActive = true;
		EventIndex = 0;

		base.SetFocus(this.transform);
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

	/// <summary>
	/// 敵作成
	/// </summary>
	void CreateEnemy() {

		var enemy = Instantiate(Enemy[EventIndex].Enemy);
		UsedInitData eneData = new UsedInitData();
		eneData.BasePosition = this.transform;
		enemy.InitEnemy(eneData);
		for (int i = 0; i < Enemy.Length; i++) {
			enemy.AddTarget(Enemy[EventIndex].Route1, true);
			enemy.AddTarget(Enemy[EventIndex].Route2);
		}
	}

	/// <summary>
	/// 時間更新
	/// </summary>
	void TimeUpdate() {


		ElapsedTime -= Time.deltaTime;
		if (ElapsedTime < 0) {

			ElapsedTime += PopInterval;
			CreateEnemy();
			EventIndex++;

			if (EventIndex >= Enemy.Length) {
				EventEnd();
			}
		}
	}

	[System.Serializable]
	public class EnemyData {

		[SerializeField] public MoveFixedEnemy Enemy;
		[SerializeField] public Transform Route1;
		[SerializeField] public Transform Route2;

	}
}
