using EDO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceGameEnd : ProduceEventBase {



	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================

	//========================================================================================
	//                                 public - override
	//========================================================================================

	public override void ProduceStart() {

		SoundManager.Instance.StopBGM(SoundManager.BGMType.GAME_BOSS);
		PlaySE();
		SceneEventManager.Instance.GameEnd();
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

	private void ProduceEnd() {

		

	}
}
