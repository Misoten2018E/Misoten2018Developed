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
		CameraManager.Instance.FocusCamera.AddTarget(bossObject.transform);

		base.EventStart();
	}

	public override bool IsEnd {
		get {
			return false;
		}

		protected set {
			base.IsEnd = value;
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	BossEnemy bossObject;
}
