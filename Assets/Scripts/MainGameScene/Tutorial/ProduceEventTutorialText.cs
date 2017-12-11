using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceEventTutorialText : ProduceEventTutorialBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private ProduceEventTutorialBase nextTutorial;

	[SerializeField] private float AppearTime = 3f;

	//========================================================================================
	//                                     public
	//========================================================================================



	//========================================================================================
	//                                    override
	//========================================================================================

	/// <summary>
	/// 演出開始
	/// </summary>
	public override void ProduceStart() {

		InitializeProduce();

	}

	//========================================================================================
	//                                     private
	//========================================================================================

	/// <summary>
	/// 演出系の初期化
	/// </summary>
	private void InitializeProduce() {


	}

	/// <summary>
	/// 演出終了
	/// </summary>
	private void ProduceEnd() {

		if (true) {

		}
	}
}
