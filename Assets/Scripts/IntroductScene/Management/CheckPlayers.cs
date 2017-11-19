using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayers : MonoBehaviour, IFIntroStartEvent {


	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// 準備チェック
	/// </summary>
	public void CheckReady() {

		for (int i = 0; i < playerList.Count; i++) {

			// 誰かがOKでないなら
			if (!playerList[i].isReadyOK) {
				return;
			}
		}

		StartMainGameScene();
		
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	public void StartEvent() {

		isEnd = false;

		var players = GetComponentsInChildren<IntroPlayer>();
		playerList.AddRange(players);

		for (int i = 0; i < playerList.Count; i++) {
			playerList[i].CheckStart(this);
		}
	}

#if UNITY_DEBUG

	void Update() {

		if (Input.GetKeyDown(KeyCode.Space)) {
			StartMainGameScene();
		}
	}

#endif

	//========================================================================================
	//                                    private
	//========================================================================================

	List<IntroPlayer> playerList = new List<IntroPlayer>();

	/// <summary>
	/// メインのゲームに移る
	/// </summary>
	void StartMainGameScene() {

		if (isEnd) {
			return;
		}

		IntroductManager.Instance.NextSceneStart();
		print("Nextscene OK");
		isEnd = true;
	}


	bool _isEnd;
	public bool isEnd {
		private set { _isEnd = value; }
		get { return _isEnd; }
	}
      
}
