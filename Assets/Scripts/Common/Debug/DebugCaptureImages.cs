using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCaptureImages : MonoBehaviour, DebuggableObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] RawImage image;

	//========================================================================================
	//                                     public
	//========================================================================================

	public static void SetImages(Texture2D tex) {
#if UNITY_DEBUG
		myInstance.image.texture = tex;
#endif
	}


	// シングルトンインスタンス
	static DebugCaptureImages myInstance;
	static public DebugCaptureImages Instance {
		get {
			return myInstance;
		}
	}


	//========================================================================================
	//                                    override
	//========================================================================================

	public void Debug(bool isDebugMode) {
		gameObject.SetActive(isDebugMode);
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	private void Start() {
		myInstance = this;
	}

	
}
