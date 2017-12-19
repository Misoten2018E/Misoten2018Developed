using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceWaveNotice : ProduceEventBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("使用する演出オブジェクトをセット")]
	[SerializeField] AnimatorControllStandard NoticeWindow;

	//========================================================================================
	//                                     public
	//========================================================================================

	//========================================================================================
	//                                 public - override
	//========================================================================================

	public override void ProduceStart() {

		NoticeWindow.gameObject.SetActive(true);
		NoticeWindow.AnimationEndCallback = AnimationEnd;
		NoticeWindow.AnimationStart();

		PlaySE();
	}


	//========================================================================================
	//                                     private
	//========================================================================================

	void AnimationEnd() {

		NoticeWindow.gameObject.SetActive(false);
	}

}
