using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImages : MonoBehaviour {



	//========================================================================================
	//                                    inspector
	//========================================================================================

	//========================================================================================
	//                                     public
	//========================================================================================


	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Start() {

		var r = GetComponentsInChildren<RawImage>();
		imageList.AddRange(r);

		CaptureImageSet();
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	private void CaptureImageSet() {

		var inst = CheckProducePhotoCamera.Instance;

#if UNITY_DEBUG
		if (inst == null) {
			return;
		}
#endif

		var caplist = inst.PhotoList;

		for (int i = 0; i < caplist.Count; i++) {
			imageList[i].texture = caplist[i];
		}
	}






	List<RawImage> imageList = new List<RawImage>();
}
