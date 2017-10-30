﻿using System.Collections;
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
