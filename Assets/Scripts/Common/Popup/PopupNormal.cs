using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNormal : MonoBehaviour ,IFPopUp{



	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private Image BG;
	[SerializeField] private Image Front;
	[SerializeField] private Text DownText;

	public void Initialize() {
		ChildActive(false);
	}


	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// 起動
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="offsetX"></param>
	/// <param name="offsetY"></param>
	public void Open(int x, int y, int offsetX = 0, int offsetY = 0) {

		transform.localPosition = new Vector3(x + offsetX, y + offsetY);
		gameObject.SetActive(true);
		SetString();

		StartCoroutine(IEFloatingImages(1f));
	}

	/// <summary>
	/// 起動
	/// </summary>
	/// <param name="trs3D"></param>
	/// <param name="offsetX"></param>
	/// <param name="offsetY"></param>
	public void Open(Transform trs3D, int offsetX = 0, int offsetY = 0) {

		var Pos2d = RectTransformUtility.WorldToScreenPoint(Camera.main, trs3D.position);

		transform.localPosition = new Vector3(Pos2d.x + offsetX, Pos2d.y + offsetY);
		gameObject.SetActive(true);
		SetString();

		StartCoroutine(IEFloatingImages(1f));
	}

	/// <summary>
	/// テキスト入力
	/// </summary>
	/// <param name="str"></param>
	public void SetString(string str = "") {

		if (DownText != null) {
			DownText.text = str;
		}
	}


	/// <summary>
	/// 終了
	/// </summary>
	public void Close() {

		StartCoroutine(IEFloatingImages(0f, () => { ChildActive(false); }) );
	}




	//========================================================================================
	//                                    private
	//========================================================================================



	/// <summary>
	/// 全てをoffに
	/// </summary>
	private void ChildActive(bool active) {

		if (BG != null) {
			BG.gameObject.SetActive(active);
		}
		if (Front != null) {
			Front.gameObject.SetActive(active);
		}
		if (DownText != null) {
			DownText.gameObject.SetActive(active);
		}
		gameObject.SetActive(false);
	}

	/// <summary>
	/// 浮き出るイメージでの表示
	/// </summary>
	/// <param name="MaxTime"></param>
	/// <returns></returns>
	IEnumerator IEFloatingImages(float MaxTime, System.Action EndCallback = null) {

		float time = 0f;
		while (true) {

			if (time >= MaxTime) {
				break;
			}

			ChangeAlphaChilds(time / MaxTime);

			time += Time.deltaTime;
			yield return null;
		}

		ChangeAlphaChilds(1f);

		if (EndCallback != null) {
			EndCallback();
		}
	}

	/// <summary>
	/// 消えていくイメージでの表示
	/// </summary>
	/// <param name="MaxTime"></param>
	/// <returns></returns>
	IEnumerator IEFadeAwayImages(float MaxTime ,System.Action EndCallback = null) {

		float time = 0f;
		while (true) {

			if (time >= MaxTime) {
				break;
			}

			ChangeAlphaChilds(1f - (time / MaxTime));

			time += Time.deltaTime;
			yield return null;
		}

		ChangeAlphaChilds(0f);
		
	}


	/// <summary>
	/// 子のアルファ変更(0～1)
	/// </summary>
	/// <param name="alpha"></param>
	private void ChangeAlphaChilds(float alpha) {

		Color buffC = new Color();

		if (BG != null) {
			buffC = BG.color;
			buffC.a = alpha;
			BG.color = buffC;
		}

		if (Front != null) {
			buffC = Front.color;
			buffC.a = alpha;
			Front.color = buffC;
		}

		if (DownText != null) {
			buffC = DownText.color;
			buffC.a = alpha;
			DownText.color = buffC;
		}
	}
}
