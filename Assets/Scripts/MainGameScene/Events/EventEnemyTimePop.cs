using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventEnemyTimePop<T> : TimelineEventStandard {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] protected T[] Enemy;
	[SerializeField] private float PopInterval = 1f;

	//========================================================================================
	//                                    public
	//========================================================================================

	override public void EventEnd() {

		IsActive = false;
		IsEnd = true;

		base.EventEnd();
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	/// <summary>
	/// オブジェクト生成時
	/// </summary>
	virtual protected void Awake() {
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

		base.EventStart();
	}


	virtual protected void Update() {

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
	protected int EventIndex;

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
	protected virtual void CreateEnemy() {

		//var enemy = Instantiate(Enemy[EventIndex].Enemy);
		//UsedInitData eneData = new UsedInitData();
		//eneData.BasePosition = this.transform;
		//enemy.InitEnemy(eneData);

		//enemy.AddTarget(Enemy[EventIndex].Route1, true);
		//enemy.AddTarget(Enemy[EventIndex].Route2);

		//PopEnemyList.Add(enemy);
	}

	/// <summary>
	/// 時間更新
	/// </summary>
	protected void TimeUpdate() {


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
	protected bool isAllEnemyDeath {
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

	protected List<EnemyTypeBase> PopEnemyList = new List<EnemyTypeBase>();
}
