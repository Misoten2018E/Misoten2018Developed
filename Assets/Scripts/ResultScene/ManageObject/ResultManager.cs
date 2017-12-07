using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

	//========================================================================================
	//                                    inspector
	//========================================================================================


	//========================================================================================
	//                                    public
	//========================================================================================

	// シングルトンインスタンス
	static ResultManager myInstance;
	static public ResultManager Instance {
		get {
			return myInstance;
		}
	}

	public void NextSceneStart() {

		var fade = GameObject.FindObjectOfType<SceneFade>();
		fade.FadeOut(() => { GameSceneManager.Instance.PermitLoad = true; });

		GameSceneManager.Instance.PermitLoad = false;

		GameSceneManager.Instance.LoadScene(GameSceneManager.SceneType.Intro, () => { EndResultScene(); });
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;
	}

	private void Start() {

		var objs = GameObjectExtensions.FindObjectOfInterface<IFResultStartEvent>();
		StartEventList.AddRange(objs);

		for (int i = 0; i < StartEventList.Count; i++) {
			StartEventList[i].StartEvent();
		}
	}

	void Update() {

		
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	List<IFResultStartEvent> StartEventList = new List<IFResultStartEvent>();

	private void EndResultScene() {

		GameSceneManager.Instance.UnloadScene(GameSceneManager.SceneType.Main, () =>
		{
			print("リザルトシーンの削除");
			GameSceneManager.Instance.UnloadScene(GameSceneManager.SceneType.Result);
		});

		
	}
}

public interface IFResultStartEvent {

	void StartEvent();
}

public interface IFResultEndEvent {

	void StartEvent();
}
