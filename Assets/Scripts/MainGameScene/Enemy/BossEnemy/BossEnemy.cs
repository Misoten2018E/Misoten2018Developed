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


}
