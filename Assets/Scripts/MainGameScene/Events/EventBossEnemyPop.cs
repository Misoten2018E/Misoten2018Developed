using EDO;
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

		SoundManager.Instance.StopBGM(SoundManager.BGMType.GAME_MAIN);
		
		SoundManager.Instance.PlaySE(SoundManager.SEType.BossStart, Vector3.zero);

		StartCoroutine(GameObjectExtensions.DelayMethod(1f, DelayBGMStart));

		base.EventStart();
	}

	public override bool IsEnd {
		get {
			return false;
		}

		protected set {
			
		}
	}

	private void DelayBGMStart() {
		SoundManager.Instance.PlayBGM(SoundManager.BGMType.GAME_BOSS);
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	BossEnemy bossObject;
}
