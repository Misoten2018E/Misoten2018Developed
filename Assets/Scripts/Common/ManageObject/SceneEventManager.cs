using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventManager : MonoBehaviour {

	//========================================================================================
	//                                    public
	//========================================================================================

	// シングルトンインスタンス
	static SceneEventManager myInstance;
	static public SceneEventManager Instance {
		get {
			return myInstance;
		}
	}

	/// <summary>
	/// 初期処理
	/// </summary>
	public void Initialize() {

		var list = GameObject.FindObjectsOfType<SceneStartEvent>();
		if (list.Length > 0) {
			ObjectList.AddRange(list);
		}
	}

	/// <summary>
	/// シーン開始
	/// </summary>
	public void SceneStart() {

		StartCoroutine(IESceneInitialize());

		
	}

	/// <summary>
	/// ゲーム開始
	/// </summary>
	public void GameStart() {

		EventManager<IFGameStartEvent>.Instance.MethodStart((IFGameStartEvent ev) => { ev.GameStart(); });
	}

	/// <summary>
	/// ゲーム終了時
	/// </summary>
	public void GameEnd() {

		EventManager<IFGameEndEvent>.Instance.MethodStart((IFGameEndEvent ev) => { ev.GameEnd(); });

		StartCoroutine(IEEndProduceCheck());
	}

	public void NextSceneStart() {

		var pauses = GameObject.FindObjectsOfType<PauseSupport>();

		for (int i = 0; i < pauses.Length; i++) {
			pauses[i].OnPause();
		}

		GameSceneManager.Instance.LoadScene(GameSceneManager.SceneType.Result,()=> {
			GameSceneManager.Instance.SetActiveScene(GameSceneManager.SceneType.Result);
		});
	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	/// <summary>
	/// オブジェクト作成時
	/// </summary>
	private void Awake() {
		myInstance = this;
	}

	/// <summary>
	/// Unityから呼ばれる初期化処理
	/// </summary>
	private void Start() {

		Initialize();

		SceneStart();
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	List<SceneStartEvent> ObjectList = new List<SceneStartEvent>();


	/// <summary>
	/// 初期処理コルーチン
	/// </summary>
	/// <returns></returns>
	IEnumerator IESceneInitialize() {

		yield return new WaitForSeconds(0.5f);

		GameSceneManager.Instance.SetActiveScene(GameSceneManager.SceneType.Main);

		yield return IESceneMyInit();

		yield return IESceneOtherInit();

		yield return IESceneDelayInit();

		print("ゲーム開始");
		GameStart();
	}


	/// <summary>
	/// オブジェクト単位の初期処理開始
	/// </summary>
	/// <returns></returns>
	IEnumerator IESceneMyInit() {

		for (int i = 0; i < ObjectList.Count; i++) {
			ObjectList[i].SceneMyInit();
		}

		yield return null;
	}

	/// <summary>
	/// 関連の初期処理開始
	/// </summary>
	/// <returns></returns>
	IEnumerator IESceneOtherInit() {

		for (int i = 0; i < ObjectList.Count; i++) {
			ObjectList[i].SceneOtherInit();
		}

		yield return null;
	}

	/// <summary>
	/// 遅延の初期処理開始
	/// </summary>
	/// <returns></returns>
	IEnumerator IESceneDelayInit() {

		for (int i = 0; i < ObjectList.Count; i++) {
			ObjectList[i].SceneDelayInit();
		}

		yield return null;
	}

	/// <summary>
	/// 終了演出完了待ち
	/// </summary>
	/// <returns></returns>
	IEnumerator IEEndProduceCheck() {

		var checkInst = EventManager<IFGameEndProduceCheck>.Instance;

		while (true) {

			var isOk = checkInst.MethodCheck((IFGameEndProduceCheck ev) => { return ev.IsEndProduce(); });
			if (isOk) {
				break;
			}

			yield return null;
		}

		NextSceneStart();
	}
}
