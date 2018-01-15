using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] Image PauseMenu;

	//========================================================================================
	//                                     public
	//========================================================================================

	public void Pause() {

		if (IsPause) {
			return;
		}

		PauseMenu.gameObject.SetActive(true);
		IsPause = true;

		PauseObjs = FindObjectsOfType<PauseSupport>();
		for (int i = 0; i < PauseObjs.Length; i++) {
			PauseObjs[i].OnPause();
		}

		Time.timeScale = 0f;
	}

	public void Resume() {

		if (PauseObjs == null) {
			return;
		}

		PauseMenu.gameObject.SetActive(false);
		IsPause = false;

		for (int i = 0; i < PauseObjs.Length; i++) {
			PauseObjs[i].OnResume();
		}

		PauseObjs = null;
		Time.timeScale = 1f;
	}

	// シングルトンインスタンス
	static PauseManager myInstance;
	static public PauseManager Instance {
		get {
			return myInstance;
		}
	}

	//========================================================================================
	//                                    override
	//========================================================================================
	// Use this for initialization
	void Start() {

		myInstance = this;
		IsPause = false;
	}


	//========================================================================================
	//                                     private
	//========================================================================================

	PauseSupport[] PauseObjs;


	bool _IsPause;
	public bool IsPause {
		private set { _IsPause = value; }
		get { return _IsPause; }
	}
      
}
