using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceWaveNotice : ProduceEventBase {


	//========================================================================================
	//                                    inspector
	//========================================================================================

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
	}


	//========================================================================================
	//                                     private
	//========================================================================================

	void AnimationEnd() {

		NoticeWindow.gameObject.SetActive(false);
	}

}
