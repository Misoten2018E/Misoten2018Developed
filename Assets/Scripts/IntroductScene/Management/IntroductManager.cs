using EDO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================


	//========================================================================================
	//                                    public
	//========================================================================================

	// シングルトンインスタンス
	static IntroductManager myInstance;
	static public IntroductManager Instance {
		get {
			return myInstance;
		}
	}

	public void NextSceneStart() {

		var fade = GameObject.FindObjectOfType<SceneFade>();
		fade.FadeOut(()=> { GameSceneManager.Instance.PermitLoad = true; });

		GameSceneManager.Instance.PermitLoad = false;
		GameSceneManager.Instance.LoadScene(GameSceneManager.SceneType.Main,()=> { EndIntroScene(); });
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;
	}

	private void Start() {

		var objs = GameObjectExtensions.FindObjectOfInterface<IFIntroStartEvent>();
		StartEventList.AddRange(objs);

		for (int i = 0; i < StartEventList.Count; i++) {
			StartEventList[i].StartEvent();
		}

		SoundManager.Instance.PlayBGM(SoundManager.BGMType.TITLE);
	}

	private void Update() {

		if (Input.GetKeyUp(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	List<IFIntroStartEvent> StartEventList = new List<IFIntroStartEvent>();

	private void EndIntroScene() {

		GameSceneManager.Instance.UnloadScene(GameSceneManager.SceneType.Intro);
		SoundManager.Instance.StopBGM(SoundManager.BGMType.TITLE);
	}
}


public interface IFIntroStartEvent {

	void StartEvent();
}
