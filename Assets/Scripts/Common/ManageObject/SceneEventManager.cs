using EDO;
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

		SoundManager.Instance.PlaySE(SoundManager.SEType.GameStart, Vector3.zero);
		
		StartCoroutine(GameObjectExtensions.DelayMethod(4f, DelayBGMStart));
	}

	private void DelayBGMStart() {

		EventManager<IFGameStartEvent>.Instance.MethodStart((IFGameStartEvent ev) => { ev.GameStart(); });

		SoundManager.Instance.PlayBGM(SoundManager.BGMType.GAME_MAIN);
	}

	/// <summary>
	/// ゲーム終了時
	/// </summary>
	public void GameEnd() {

		EventManager<IFGameEndEvent>.Instance.MethodStart((IFGameEndEvent ev) => { ev.GameEnd(); });

		StartCoroutine(IEEndProduceCheck());
	}

	/// <summary>
	/// シーンを遷移させる
	/// デバッグ時にのみ基本的には呼ばれる
	/// </summary>
	public void NextSceneStart() {

		var pauses = GameObject.FindObjectsOfType<PauseSupport>();

		for (int i = 0; i < pauses.Length; i++) {
			pauses[i].OnPause();
		}

		SetReslutShareData();

		var gameMng = GameSceneManager.Instance;

		var fade = GameObject.FindObjectOfType<SceneFade>();
		fade.FadeOut(() => { gameMng.PermitLoad = true; });

		gameMng.PermitLoad = false;

		SoundManager.Instance.StopBGM(SoundManager.BGMType.GAME_MAIN);
		SoundManager.Instance.StopBGM(SoundManager.BGMType.GAME_BOSS);

		gameMng.LoadScene(GameSceneManager.SceneType.Result,()=> {

			// メインシーンの破棄
			gameMng.UnloadScene(GameSceneManager.SceneType.Main);

			gameMng.SetActiveScene(GameSceneManager.SceneType.Result);
		//	fade.gameObject.SetActive(false);
		});
	}


	public void TitleSceneStart() {

		var pauses = GameObject.FindObjectsOfType<PauseSupport>();

		for (int i = 0; i < pauses.Length; i++) {
			pauses[i].OnPause();
		}

		var gameMng = GameSceneManager.Instance;

		var fade = GameObject.FindObjectOfType<SceneFade>();
		fade.FadeOut(() => { gameMng.PermitLoad = true; });

		gameMng.PermitLoad = false;

		SoundManager.Instance.StopBGM(SoundManager.BGMType.GAME_MAIN);
		SoundManager.Instance.StopBGM(SoundManager.BGMType.GAME_BOSS);

		gameMng.LoadScene(GameSceneManager.SceneType.Intro, () => {

			// メインシーンの破棄
			gameMng.UnloadScene(GameSceneManager.SceneType.Main);

			gameMng.SetActiveScene(GameSceneManager.SceneType.Intro);
		//	fade.gameObject.SetActive(false);
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

#if UNITY_DEBUG
		if (GameSceneManager.Instance != null) {
			GameSceneManager.Instance.SetActiveScene(GameSceneManager.SceneType.Main);
		}
#else
		if (GameSceneManager.Instance != null) {
			GameSceneManager.Instance.SetActiveScene(GameSceneManager.SceneType.Main);
		}
		//GameSceneManager.Instance.SetActiveScene(GameSceneManager.SceneType.Main);
#endif


		yield return IESceneMyInit();

		yield return IESceneOtherInit();

		yield return IESceneDelayInit();

		print("ゲーム開始");
		GameStart();
	}

	/// <summary>
	/// リザルトへ受け渡すデータ群
	/// </summary>
	private void SetReslutShareData() {

		var data = new SceneToSceneDataSharing.MainToResult();

		data.CityLevel = City.Instance.CityLevel;

		var players = PlayerManager.instance.PlayersObject;

		data.Player1_LastSta = players[0].GetComponent<Player>().GetCharacterSta();
		data.Player2_LastSta = players[1].GetComponent<Player>().GetCharacterSta();
		data.Player3_LastSta = players[2].GetComponent<Player>().GetCharacterSta();
		data.Player4_LastSta = players[3].GetComponent<Player>().GetCharacterSta();

		data.textureLists = new List<Texture2D>(CheckProducePhotoCamera.Instance.PhotoList);

		data.PlayersScore = new int[4];

		for (int i = 0; i < 4; i++) {
			data. PlayersScore[i] = Score.instance.GetPlayerScore(i);
		}

		SceneToSceneDataSharing.Instance.mainToResultData = data;
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

		yield return null;

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
