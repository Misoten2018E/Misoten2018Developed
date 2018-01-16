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


	//public void SetInput(MultiInput input) {
	//	multiInput = input;
	//}

	//========================================================================================
	//                                    override
	//========================================================================================
	// Use this for initialization
	void Start() {

		myInstance = this;
		IsPause = false;
		multiInput = GetComponent<MultiInput>();
	}


	private void Update() {

		CheckPauseEvent();
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

	MultiInput multiInput;

	protected void CheckPauseEvent() {

#if UNITY_DEBUG
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (IsPause) {
				Resume();
				CheckResetGameEditor();
			}
			else {
				Pause();
			}
		}
#endif
		if (multiInput == null) {
			return;
		}

		if (multiInput.GetAllButtonPauseTrigger()) {

			if (IsPause) {
				Resume();
				CheckResetGame();
			}
			else {
				Pause();
			}
		}
	}


	bool CheckResetGame() {

		bool circie = multiInput.GetButtonCirclePress();
		bool cross = multiInput.GetButtonCrossPress();
		bool triangle = multiInput.GetButtonTrianglePress();

		if (circie && cross && triangle) {

			SceneEventManager.Instance.TitleSceneStart();
			return true;
		}
		return false;
	}

	bool CheckResetGameEditor() {

		bool circie = Input.GetKey(KeyCode.Q);
		bool cross = true;//Input.GetKey(KeyCode.E);
		bool triangle = Input.GetKey(KeyCode.X);

		if (circie && cross && triangle) {

			SceneEventManager.Instance.TitleSceneStart();
			return true;
		}
		return false;
	}
}
