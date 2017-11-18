using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MoveTargetEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float SpownTime = 60;

	

	//========================================================================================
	//                                     public
	//========================================================================================

	public void InitBossEnemy(BossCheckPointManager mng) {

		CheckMng = mng;

		CameraManager.Instance.FocusCamera.AddTarget(transform);

		NextTarget = CheckMng.GetNextRelayPoint(null);
		BossSpeed = CheckMng.CalcSpeedPointToPoint(SpownTime);
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================


	// Use this for initialization
	void Start() {



	}

	// Update is called once per frame
	void Update() {

	}

	public override void InitEnemy(UsedInitData InitData) {
		throw new System.NotImplementedException();
	}

	public override bool IsDeath {
		get { return false; }
		protected set { throw new System.NotImplementedException(); }
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	BossCheckPointManager CheckMng;

	RelayPoint NextTarget;

	float BossSpeed;

	void MoveTarget() {

		MoveAdvanceToTarget(NextTarget.transform, BossSpeed);

	}
}
