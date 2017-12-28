using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayers : MonoBehaviour, IFIntroStartEvent {
    const float max_time = 1.5f;
    float nowtime;
    bool CheckOK;

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

        CheckOK = true;

        //一定時間後にシーンチェンジにするためコメントアウト
       // StartMainGameScene();
		
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	public void StartEvent() {

		isEnd = false;
        nowtime = 0;
        CheckOK = false;

        var players = GetComponentsInChildren<IntroPlayer>();
		playerList.AddRange(players);

		for (int i = 0; i < playerList.Count; i++) {
			playerList[i].CheckStart(this);
		}

    }



	void Update() {

#if UNITY_DEBUG
        if (Input.GetKeyDown(KeyCode.Space)) {
			StartMainGameScene();
		}
#endif
		if (CheckOK) {
			nowtime += Time.deltaTime;

			if (nowtime > max_time) {
                //StartMainGameScene();
                IntroManager IntroM;
                IntroM = GameObject.Find("UIRoot").transform.Find("IntroManage").gameObject.GetComponent<IntroManager>();
                
                IntroM.ManagerAniON();
            }
		}
	}


	//========================================================================================
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
