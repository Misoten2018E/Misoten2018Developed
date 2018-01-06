using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImages : MonoBehaviour {

    Animator ani;

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
	//void Start() {

		
	//}

    private void Awake()
    {
        var r = GetComponentsInChildren<RawImage>();
        imageList.AddRange(r);

        CaptureImageSet();

        ani = GetComponent<Animator>();
        ani.enabled = false;
    }

    //========================================================================================
    //                                     private
    //========================================================================================

    private void CaptureImageSet() {

		//var inst = CheckProducePhotoCamera.Instance;
        var inst = SceneToSceneDataSharing.Instance;

#if UNITY_DEBUG
        if (inst == null) {
			return;
		}
#endif

		var caplist = inst.mainToResultData.textureLists;

        if (caplist == null)
        {
            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].enabled = false;
            }

            return;
        }

		for (int i = 0; i < imageList.Count; i++) {
            if (i >= caplist.Count)
            {
                imageList[i].enabled = false;

                continue;
            }

            if (caplist[i] != null)
            {
                imageList[i].texture = caplist[i];
            }
            

            imageList[i].enabled = false;
        }
	}

    public void CaptureON()
    {
        for (int i = 0; i < imageList.Count; i++)
        {
         
            if (imageList[i].texture != null)
            {
                imageList[i].enabled = true;
            }

        }

        ani.enabled = true;
    }




    List<RawImage> imageList = new List<RawImage>();
}
