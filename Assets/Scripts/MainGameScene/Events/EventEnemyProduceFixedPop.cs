using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyProduceFixedPop : EventEnemyTimePop<EventEnemyProduceFixedPop.EnemyData> {


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

	public override void EventStart() {

		IsEnd = false;
		EventIndex = 0;

		base.SetFocus(this.transform);

		enemyEgg = CreateEgg();
		StartCoroutine(GameObjectExtensions.DelayMethod(ProduceTime*1.5f, ()=> {
			IsActive = true;
			}));

		AwakeProduceStart();
	}

	public override void EventEnd() {

		enemyEgg.EndEvent();
		base.EventEnd();
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	const float ProduceTime = 1f;

	EventEnemyEgg enemyEgg;

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


	/// <summary>
	/// たまご作成
	/// </summary>
	private EventEnemyEgg CreateEgg() {

		var pre = ResourceManager.Instance.Get<EventEnemyEgg>(ConstDirectry.DirPrefabsEnemy, ConstString.EnemyEgg);
		var egg = Instantiate(pre);
		egg.EventStart(new Vector3(0, 25f, -50f), transform.position, ProduceTime);
		return egg;
	}
	

	[System.Serializable]
	public class EnemyData {

		[SerializeField] public MoveFixedEnemy Enemy;
		[SerializeField] public Transform Route1;
		[SerializeField] public Transform Route2;

	}

}
