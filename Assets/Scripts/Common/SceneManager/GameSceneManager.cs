using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
public class GameSceneManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private SceneStatus nowScene;

	//========================================================================================
	//                                    public
	//========================================================================================

	static private GameSceneManager myInstance;
	public static GameSceneManager Instance {
		get {
			return myInstance;
		}
	}

	public static int sceneId {
		get { return (int)Instance.nowScene; }
	}


	public SceneStatus sceneStatus {
		set { nowScene = value; }
		get { return nowScene; }
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

		// グローバルシーンなら初期化処理開始
		if (nowScene == SceneStatus.Global) {

			nowScene = SceneStatus.Title;
			StartCoroutine(IESceneLoad(Loader, ConstScene.MainGameScene));
		}
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

	/// <summary>
	/// シーンの変更
	/// </summary>
	/// <param name="next"></param>
	public void SceneChange(SceneStatus next) {

		nowScene = next;

	}

	public enum SceneStatus {
		Global,
		Title,
		StageSelect,
		Game,
		SceneMax
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	private IEnumerator SceneEnd(System.Func<bool> act) {

		// アクションが終わらない間
		while (!act()) {

			yield return new WaitForSeconds(0.1f);

		}

	}
}