using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
public class GameSceneManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	public SceneType StartScene = SceneType.Intro;

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
	protected SceneLoader Loader {
		get {
			if (_loader == null) {
				_loader = GetComponent<SceneLoader>();
			}
			return _loader;
		}
	}

	public void SetActiveScene(SceneType type) {

		var scene = LoadSceneList[(int)type];

		if (scene == null) {
			DebugNullSceneMessage();
			return;
		}
		SceneManager.SetActiveScene(scene);
	}

	private static void DebugNullSceneMessage() {
		Debug.LogError("シーンが存在しない");
	}

	/// <summary>
	/// シーンのロード
	/// </summary>
	/// <param name="type"></param>
	public void LoadScene(SceneType type, System.Action EndCallBack = null) {

		EndLoadCallback = EndCallBack;

		switch (type) {

			case SceneType.Intro:
				StartCoroutine(IESceneLoad(ConstScene.IntroScene, type));
				break;
			case SceneType.Main:
				StartCoroutine(IESceneLoad(ConstScene.MainGameScene, type));
				break;

			case SceneType.Result:
				StartCoroutine(IESceneLoad(ConstScene.ResultScene, type));
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// シーンのアンロード
	/// </summary>
	/// <param name="type"></param>
	public void UnloadScene(SceneType type, System.Action EndCallBack = null) {

		EndLoadCallback = EndCallBack;

		StartCoroutine(IESceneUnload(type, EndCallBack));

	}


	//========================================================================================
	//                                    public - override
	//========================================================================================

	/// <summary>
	/// 開始
	/// </summary>
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

		LoadSceneList[(int)SceneType.Global] = SceneManager.GetActiveScene();
		LoadScene(StartScene);
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	/// <summary>
	/// シーン終了用コルーチン
	/// 特殊シーンでない限りディレクトリの指定不要
	/// </summary>
	/// <returns></returns>
	private IEnumerator IESceneLoad(string sceneName ,SceneType type ,string directry = ConstDirectry.DirScene) {

		var loader = Loader;

		yield return null;

		// 次のシーンのロード
		loader.SceneLoad(directry, sceneName);

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
		LoadSceneList[(int)type] = loader.LoadingScene;

		if(StartScene == type){
			SceneManager.SetActiveScene(LoadSceneList[(int)type]);
		}

		yield return null;

		if (EndLoadCallback != null) {
			EndLoadCallback();
			EndLoadCallback = null;
		}
	}

	/// <summary>
	/// シーン終了用コルーチン
	/// 特殊シーンでない限りディレクトリの指定不要
	/// </summary>
	/// <returns></returns>
	private IEnumerator IESceneUnload(SceneType type,System.Action EndCallback = null) {

		var loader = Loader;

		Scene unloadScene = LoadSceneList[(int)type];
		if (unloadScene == null) {
			// 存在しないなら終了
			DebugMessage();
			yield break;
		}

		yield return null;

		// 次のシーンのアンロード
		loader.SceneUnload(unloadScene.name);

		yield return null;

		while (true) {

			// ロード終了したら
			if (loader.isReleased) {
				break;
			}

			yield return null;
		}

		yield return null;

		if (EndCallback != null) {
			EndCallback();
		}
	}

	private static void DebugMessage() {
		print("存在しないシーンをアンロードしようとした");
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	public enum SceneType {
		Global,
		Intro,
		Main,
		Result,
		SceneMax
	}
	const int SceneMax = (int)SceneType.SceneMax;

	Scene[] LoadSceneList = new Scene[SceneMax];

	System.Action EndLoadCallback;
}