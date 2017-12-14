using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private List<PreLoadData> PreloadList;

	//========================================================================================
	//                                    public
	//========================================================================================

	// シングルトンインスタンス
	static ResourceManager myInstance;
	static public ResourceManager Instance {
		get {
			return myInstance;
		}
	}

	/// <summary>
	/// リソースのゲット
	/// 無い場合はロード
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="directryName"></param>
	/// <param name="PrefabName"></param>
	/// <returns></returns>
	public T Get<T>(string directryName ,string PrefabName) {

		// 既にロード済みなら
		if (DictionaryList.ContainsKey(PrefabName)) {
			return DictionaryList[PrefabName].GetComponent<T>();
		}
		else {

			var obj = Load(directryName, PrefabName);

#if UNITY_DEBUG
			if (obj == null) {
				Debug.LogError(PrefabName + " : Load Error");
			}
#endif
			return obj.GetComponent<T>();
		}
	}

	/// <summary>
	/// リソースのロード
	/// </summary>
	/// <param name="directryName"></param>
	/// <param name="PrefabName"></param>
	/// <returns></returns>
	public GameObject Load(string directryName, string PrefabName) {

		var obj = Resources.Load(directryName + PrefabName) as GameObject;

		if (obj != null) {
			DictionaryList.Add(PrefabName, obj);
		}
		else {
			DebugLog.log("読み込み失敗 : " + PrefabName);
			Debug.Log("読み込み失敗 : " + PrefabName);
		}
		return obj;
	}

	private void Awake() {

		Preload();

		if (myInstance == null) {
			myInstance = this;
		}
		else {
			Destroy(this);
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	Dictionary<string, GameObject> DictionaryList = new Dictionary<string, GameObject>();

	private void Preload() {

		for (int i = 0; i < PreloadList.Count; i++) {
			var data = PreloadList[i];
			Load(data.DirectryName, data.PrefabName);
		}
	}

	[System.Serializable]
	public struct PreLoadData {
		public string DirectryName;
		public string PrefabName;
	}
}
