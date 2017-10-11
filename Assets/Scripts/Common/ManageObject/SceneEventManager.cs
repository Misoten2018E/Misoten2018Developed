using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventManager : MonoBehaviour {


	List<SceneStartEvent> ObjectList = new List<SceneStartEvent>();

	private void Start() {

		Initialize();

		SceneStart();
	}


	public void Initialize() {

		var list = GameObject.FindObjectsOfType<SceneStartEvent>();
		if (list.Length > 0) {
			ObjectList.AddRange(list);
		}
	}

	public void SceneStart() {

		StartCoroutine(SceneInitialize());
	}

	/// <summary>
	/// 初期処理コルーチン
	/// </summary>
	/// <returns></returns>
	IEnumerator SceneInitialize() {

		yield return null;

		yield return SceneMyInit();

		yield return SceneOtherInit();

		yield return SceneDelayInit();

		yield return null;
	}


	/// <summary>
	/// オブジェクト単位の初期処理開始
	/// </summary>
	/// <returns></returns>
	IEnumerator SceneMyInit() {

		for (int i = 0; i < ObjectList.Count; i++) {
			ObjectList[i].SceneMyInit();
		}

		yield return null;
	}

	/// <summary>
	/// 関連の初期処理開始
	/// </summary>
	/// <returns></returns>
	IEnumerator SceneOtherInit() {

		for (int i = 0; i < ObjectList.Count; i++) {
			ObjectList[i].SceneOtherInit();
		}

		yield return null;
	}

	/// <summary>
	/// 遅延の初期処理開始
	/// </summary>
	/// <returns></returns>
	IEnumerator SceneDelayInit() {

		for (int i = 0; i < ObjectList.Count; i++) {
			ObjectList[i].SceneDelayInit();
		}

		yield return null;
	}
}
