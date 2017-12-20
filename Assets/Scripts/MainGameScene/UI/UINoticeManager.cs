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

		SpecialActive = active;

		CityInCross.gameObject.SetActive(active);
		JobController.gameObject.SetActive(active);

	}

	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;
	}

	private void Update() {

		if (CityInNum <= 0) {
			JobController.AnimationEnd();
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	private int CityInNum;
	private bool SpecialActive;	// これがfalseの時表示を呼ばれても表示しない

	void CityInIconChange(bool active) {

		if (!SpecialActive) {
			return;
		}
		CityInCross.gameObject.SetActive(active);
	}

	/// <summary>
	/// ジョブアイコン表示
	/// </summary>
	void AppearJobIcon() {

		if (CityInNum > 0) {

			// 入った人数を増やす
			CityInNum++;
			return;
		}

		JobController.gameObject.SetActive(true);
		JobController.AnimationStart();
	}

	/// <summary>
	/// 
	/// </summary>
	void DisappearJobIcon() {

		CityInNum--;

		if (CityInNum <= 0) {
			JobController.AnimationEnd();
		}
	}
}
