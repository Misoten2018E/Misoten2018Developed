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
        //var r = GetComponentsInChildren<RawImage>();
        //imageList.AddRange(r);
        //backList.AddRange(r);
        imageList = new RawImage[8];
        backList = new RawImage[8];
        imageList[0] = transform.Find("Image1").GetComponent<RawImage>();
        imageList[1] = transform.Find("Image2").GetComponent<RawImage>();
        imageList[2] = transform.Find("Image3").GetComponent<RawImage>();
        imageList[3] = transform.Find("Image4").GetComponent<RawImage>();
        imageList[4] = transform.Find("Image5").GetComponent<RawImage>();
        imageList[5] = transform.Find("Image6").GetComponent<RawImage>();
        imageList[6] = transform.Find("Image7").GetComponent<RawImage>();
        imageList[7] = transform.Find("Image8").GetComponent<RawImage>();

        backList[0] = transform.Find("back1").GetComponent<RawImage>();
        backList[1] = transform.Find("back2").GetComponent<RawImage>();
        backList[2] = transform.Find("back3").GetComponent<RawImage>();
        backList[3] = transform.Find("back4").GetComponent<RawImage>();
        backList[4] = transform.Find("back5").GetComponent<RawImage>();
        backList[5] = transform.Find("back6").GetComponent<RawImage>();
        backList[6] = transform.Find("back7").GetComponent<RawImage>();
        backList[7] = transform.Find("back8").GetComponent<RawImage>();

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
            for (int i = 0; i < 8; i++)
            {
                imageList[i].enabled = false;
            }

            return;
        }

		for (int i = 0; i < 8; i++) {
            if (i >= caplist.Count)
            {
                imageList[i].enabled = false;
                backList[i].enabled = false;

                continue;
            }

            if (caplist[i] != null)
            {
                imageList[i].texture = caplist[i];
            }
            

            imageList[i].enabled = false;
            backList[i].enabled = false;
        }
	}

    public void CaptureON()
    {
        for (int i = 0; i < 8; i++)
        {
         
            if (imageList[i].texture != null)
            {
                imageList[i].enabled = true;
                backList[i].enabled = true;
            }

        }

        for (int i = 0; i < 8; i++)
        {
            print("name"+ imageList[i].name + imageList[i].enabled);
            print("name" + backList[i].name + backList[i].enabled);
        }

        ani.enabled = true;
    }




    //List<RawImage> imageList = new List<RawImage>();
    //List<RawImage> backList = new List<RawImage>();
    RawImage[] imageList;
    RawImage[] backList;
}
