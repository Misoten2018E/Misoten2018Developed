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

		var players = GetComponentsInChildren<IntroPlayer>();
		playerList.AddRange(players);

		for (int i = 0; i < playerList.Count; i++) {
			playerList[i].CheckStart(this);
		}
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	List<IntroPlayer> playerList = new List<IntroPlayer>();

	/// <summary>
	/// メインのゲームに移る
	/// </summary>
	void StartMainGameScene() {

		IntroductManager.Instance.NextSceneStart();
		print("Nextscene OK");
	}

	
}
