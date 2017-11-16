using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBodyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var c = GetComponentsInChildren<TrailSupport>();
		if (c.Length > 0) {
			TrailBodyList.AddRange(c);
		}
	}


	//========================================================================================
	//                                    public
	//========================================================================================


	/// <summary>
	/// ボディ毎のトレイルを返す
	/// nullの可能性有
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public TrailSupport GetBodyTrail(TrailSupport.BodyType type) {

		for (int i = 0; i < TrailBodyList.Count; i++) {
			if(TrailBodyList[i].MyBodyType == type) {
				return TrailBodyList[i];
			}
		}

		return null;
	}

	/// <summary>
	/// トレイル開始
	/// </summary>
	/// <param name="type"></param>
	public void StartTrail(TrailSupport.BodyType type) {

		for (int i = 0; i < TrailBodyList.Count; i++) {
			if (TrailBodyList[i].MyBodyType == type) {
				TrailBodyList[i].StartTrail();
				return;
			}
		}
	}

	/// <summary>
	/// トレイル終了
	/// </summary>
	/// <param name="type"></param>
	public void EndTrail(TrailSupport.BodyType type) {

		for (int i = 0; i < TrailBodyList.Count; i++) {
			if (TrailBodyList[i].MyBodyType == type) {
				TrailBodyList[i].EndTrail();
				return;
			}
		}
	}

	List<TrailSupport> TrailBodyList = new List<TrailSupport>();
}
