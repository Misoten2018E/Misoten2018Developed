using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToSceneDataSharing : MonoBehaviour {


	//========================================================================================
	//                                     public
	//========================================================================================


	// シングルトンインスタンス
	static SceneToSceneDataSharing myInstance;
	static public SceneToSceneDataSharing Instance {
		get {
			return myInstance;
		}
	}


	MainToResult _mainToResultData = new MainToResult();
	/// <summary>
	/// メインからリザルトへ渡すデータ群
	/// </summary>
	public MainToResult mainToResultData {
		set { _mainToResultData = value; }
		get { return _mainToResultData; }
	}



	private void Start() {

		
		if (myInstance != null) {
			Destroy(gameObject);
			return;
		}

		myInstance = this;
	}

	//========================================================================================
	//                                     private
	//========================================================================================


	public struct MainToResult {
		// プレイヤー最終ステータス群
		public int Player1_LastSta;
		public int Player2_LastSta;
		public int Player3_LastSta;
		public int Player4_LastSta;

		// 街レベル
		public int CityLevel;

		public List<Texture2D> textureLists;
	}
}
