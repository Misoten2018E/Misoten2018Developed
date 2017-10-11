using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	//void Start() {

	//	PermitLoading = false;
	//	isLoading = false;

	//}

	bool isLoading;         // ロード中ならtrue
	bool isPermitLoad;      // シーン遷移を許すならtrue
	bool isDestruct;        // シーン解放可能ならtrue


	private Scene loadScene;
	public Scene LoadingScene {
		get {
			return loadScene;
		}
	}

	/// <summary>
	/// シーンのロード開始
	/// </summary>
	/// <param name="name"></param>
	public void SceneLoad(string name) {

		isLoading = true;
		StartCoroutine(LoadSceneAsyncAdditive(name));
	}


	public void SceneUnload(string name) {

		isDestruct = false;
		StartCoroutine(UnLoadSceneAsync(name));
	}

	/// <summary>
	/// 読み込み直し
	/// </summary>
	public void SceneReload() {

		isLoading = true;

		StartCoroutine(IEReloadScene());

	}

	/// <summary>
	/// ロード中 = true
	/// </summary>
	public bool isLoad {
		get { return isLoading; }
	}

	/// <summary>
	/// ロードを許すかどうか
	/// 切り替え可能であればtrueにする
	/// </summary>
	public bool PermitLoading {
		set { isPermitLoad = value; }
		get { return isPermitLoad; }
	}

	/// <summary>
	/// シーン解放
	/// </summary>
	public bool isReleased {
		get { return isDestruct; }
	}

	/// <summary>
	/// シーン非同期追加開始
	/// </summary>
	/// <param name="StageName"></param>
	/// <returns></returns>
	IEnumerator LoadSceneAsyncAdditive(string StageName) {

		DebugLog.log(StageName + "ロード開始");
		isLoading = true;

		AsyncOperation ao = SceneManager.LoadSceneAsync(StageName, LoadSceneMode.Additive);
		ao.allowSceneActivation = false;

		while (true) {

			// こちらの待たせたい時間とロードが双方終わっていたら終了
			// (0.9でprogressが強制で止まるので注意)
			if ((PermitLoading) && (ao.progress >= 0.9f)) {
				break;
			}

			yield return null;
		}

		//次のレベルに遷移
		ao.allowSceneActivation = true;

		yield return null;

		print(StageName + "ロード終了");
		DebugLog.log(StageName + "ロード終了");

		loadScene = SceneManager.GetSceneByName(StageName);

		isLoading = false;

		yield return null;
	}

	/// <summary>
	/// シーン非同期削除開始
	/// </summary>
	/// <param name="StageName"></param>
	/// <returns></returns>
	IEnumerator UnLoadSceneAsync(string StageName) {

		isDestruct = false;

		yield return null;

		print(StageName + "削除開始");

		var scene = SceneManager.GetSceneByName(StageName);
		AsyncOperation ao = SceneManager.UnloadSceneAsync(scene);
		ao.allowSceneActivation = false;

		const float Wait = 0.9f;

		while (true) {

			if (PermitLoading && ao.progress >= Wait) {
				break;
			}

			yield return null;
		}

		//次のレベルに遷移
		isDestruct = true;
		ao.allowSceneActivation = true;

		print(StageName + "削除終了");


		yield return null;
	}

	/// <summary>
	/// シーン非同期削除開始
	/// </summary>
	/// <param name="StageName"></param>
	/// <returns></returns>
	IEnumerator UnLoadSceneAsync(Scene scene) {

		isDestruct = false;

		yield return null;

		print(scene.name + "削除開始");

		AsyncOperation ao = SceneManager.UnloadSceneAsync(scene);
		ao.allowSceneActivation = false;

		const float Wait = 0.9f;

		while (true) {

			if (PermitLoading && ao.progress >= Wait) {
				break;
			}

			yield return null;
		}

		//次のレベルに遷移
		isDestruct = true;
		ao.allowSceneActivation = true;

		print(scene.name + "削除終了");


		yield return null;
	}


	/// <summary>
	/// シーンのリロードを行う
	/// </summary>
	/// <returns></returns>
	IEnumerator IEReloadScene() {

		var scene = SceneManager.GetActiveScene();

		PermitLoading = true;

		StartCoroutine(LoadSceneAsyncAdditive(scene.name));

		while (true) {

			if (!this.isLoad) {
				yield return null;
				break;
			}

			yield return null;
		}


		StartCoroutine(UnLoadSceneAsync(scene));

		while (true) {

			if (this.isReleased) {
				yield return null;
				break;
			}

			yield return null;
		}
	}
}