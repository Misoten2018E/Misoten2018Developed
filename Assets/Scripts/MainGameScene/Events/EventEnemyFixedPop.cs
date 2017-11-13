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

		IsActive = false;
		IsEnd = true;
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	/// <summary>
	/// オブジェクト生成時
	/// </summary>
	private void Awake() {
		IsActive = false;
		IsEnd = false;
		EventIndex = 0;
	}

	/// <summary>
	/// イベント開始
	/// </summary>
	public override void EventStart() {

		IsActive = true;
		IsEnd = false;
		EventIndex = 0;

		base.SetFocus(this.transform);
	}


	private void Update() {

		if (!IsActive) {
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
	public bool IsActive {
		private set { _isActive = value; }
		get { return _isActive; }
	}

	bool _isEnd;
	public override bool IsEnd {
		protected set { _isEnd = value; }
		get { return _isEnd && isAllEnemyDeath; }
	}

	/// <summary>
	/// 敵作成
	/// </summary>
	void CreateEnemy() {

		var enemy = Instantiate(Enemy[EventIndex].Enemy);
		UsedInitData eneData = new UsedInitData();
		eneData.BasePosition = this.transform;
		enemy.InitEnemy(eneData);

		enemy.AddTarget(Enemy[EventIndex].Route1, true);
		enemy.AddTarget(Enemy[EventIndex].Route2);

		PopEnemyList.Add(enemy);
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

	/// <summary>
	/// 全ての敵が出現した上でやられているならtrue
	/// </summary>
	bool isAllEnemyDeath {
		get {

			for (int i = 0; i < PopEnemyList.Count; i++) {

				// 空(削除済み)でなく、死んでないなら
				if ((PopEnemyList[i] != null) && (!PopEnemyList[i].IsDeath)) {
					return false;
				}
			}

			return true;
		}
	}

	List<EnemyTypeBase> PopEnemyList = new List<EnemyTypeBase>();

	[System.Serializable]
	public class EnemyData {

		[SerializeField] public MoveFixedEnemy Enemy;
		[SerializeField] public Transform Route1;
		[SerializeField] public Transform Route2;

	}
}
