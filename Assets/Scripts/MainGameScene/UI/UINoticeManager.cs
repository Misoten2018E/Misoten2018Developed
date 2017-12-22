using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoticeManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private Image CityInCross;

	[SerializeField] private JobChangeIconController JobController;

	[SerializeField] private Mask StealthMask;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum UIType {

		CityIn,
		JobChange,
	}


	// シングルトンインスタンス
	static UINoticeManager myInstance;
	static public UINoticeManager Instance {
		get {
			return myInstance;
		}
	}

	/// <summary>
	/// アイコン開始
	/// </summary>
	/// <param name="type"></param>
	public void StartIcon(UIType type) {

		switch (type) {
			case UIType.CityIn:
				CityInIconChange(true);
				break;
			case UIType.JobChange:
				AppearJobIcon();
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// アイコン終了
	/// </summary>
	/// <param name="type"></param>
	public void CloseIcon(UIType type) {

		switch (type) {
			case UIType.CityIn:
				CityInIconChange(false);
				break;
			case UIType.JobChange:
				DisappearJobIcon();
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// 特殊な状況でのアイコン消去用
	/// </summary>
	/// <param name="active"></param>
	public void ActiveChangeIcons(bool active) {

		StealthMask.enabled = !active;
	}

	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;

		ActiveChangeIcons(true);
	}

	private void Update() {

		if (CityInChangeJobNum <= 0) {
			JobController.AnimationEnd();
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	private int CityInNum;
	private int CityInChangeJobNum;

	void CityInIconChange(bool active) {

		CityInNum += active ? 1 : -1;

		if (CityInNum > 0) {
			CityInCross.gameObject.SetActive(true);
		}
		else {
			CityInCross.gameObject.SetActive(false);
		}

		DebugId = DebugLog.ChaseLog("街範囲にいる人数 : " + CityInNum, DebugId);
	}
	int DebugId = -1;
	int DebugId2 = -1;
	/// <summary>
	/// ジョブアイコン表示
	/// </summary>
	void AppearJobIcon() {

		// 入った人数を増やす
		CityInChangeJobNum++;

		DebugId2 = DebugLog.ChaseLog("街中人数 : " + CityInChangeJobNum, DebugId2);

		if (CityInChangeJobNum > 1) {

			return;
		}

		JobController.gameObject.SetActive(true);
		JobController.AnimationStart();

	}

	/// <summary>
	/// 
	/// </summary>
	void DisappearJobIcon() {

		CityInChangeJobNum--;

		if (CityInChangeJobNum <= 0) {
			JobController.AnimationEnd();
		}

		DebugId2 = DebugLog.ChaseLog("街中人数 : " + CityInChangeJobNum, DebugId2);
	}
}
