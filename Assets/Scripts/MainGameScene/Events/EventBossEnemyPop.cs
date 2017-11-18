using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBossEnemyPop : TimelineEventStandard {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private BossCheckPointManager CheckPointManager;
	[SerializeField] private BossEnemy Boss;

	//========================================================================================
	//                                     public
	//========================================================================================

	//========================================================================================
	//                                 public - override
	//========================================================================================

	public override void EventStart() {

		bossObject = Instantiate(Boss);
		bossObject.InitBossEnemy(CheckPointManager);
		bossObject.InitEnemy(new UsedInitData());
	}

	public override bool IsEnd {
		get {
			return false;
		}

		protected set {
			base.IsEnd = value;
		}
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	//========================================================================================
	//                                     private
	//========================================================================================

	BossEnemy bossObject;
}
