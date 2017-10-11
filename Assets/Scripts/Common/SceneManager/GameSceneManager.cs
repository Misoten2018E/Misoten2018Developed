using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
public class GameSceneManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================


	//========================================================================================
	//                                    public
	//========================================================================================

	static private GameSceneManager myInstance;
	public static GameSceneManager Instance {
		get {
			return myInstance;
		}
	}


	SceneLoader _loader;
	public SceneLoader Loader {
		get {
			if (_loader == null) {
				_loader = GetComponent<SceneLoader>();
			}
			return _loader;
		}
	}



	// Use this for initialization
	void Start() {

		// 存在していたら再生成しない
		if (myInstance == null) {
			myInstance = this;
			DontDestroyOnLoad(this);
		}
		else {
			Destroy(this);
			Destroy(gameObject);
			return;
		}

		// FPS固定
		Application.targetFrameRate = 60;


		// ロードを許可
		Loader.PermitLoading = true;

		StartCoroutine(IESceneLoad(Loader, ConstScene.MainGameScene));
	}

	/// <summary>
	/// シーン終了用コルーチン
	/// </summary>
	/// <returns></returns>
	private IEnumerator IESceneLoad(SceneLoader loader, string sceneName) {

		yield return null;

		// 次のシーンのロード
		loader.SceneLoad(sceneName);

		yield return null;

		while (true) {

			// ロード終了したら
			if (!loader.isLoad) {
				break;
			}

			yield return null;
		}
		yield return null;

		// ロードしたシーンをアクティブに
		var scene = loader.LoadingScene;
		SceneManager.SetActiveScene(scene);

		yield return null;
	}


	//========================================================================================
	//                                    private
	//========================================================================================

}