using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupedSubCamera : MonoBehaviour,IFPopUp {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float OpenTime = 0.5f;
	[SerializeField] private AnimationCurve CurveX, CurveY;

	//========================================================================================
	//                                    public - override
	//========================================================================================

	public void Open(int x, int y, int offsetX = 0, int offsetY = 0) {

		rectTrs.localPosition = new Vector3(x, y);
		gameObject.SetActive(true);

		ieOpenWindow = IEOpenedWindow(OpenTime);
		StartCoroutine(ieOpenWindow);
	}

	public void Open(Transform trs3D, int offsetX = 0, int offsetY = 0) {

		var Pos2d = RectTransformUtility.WorldToScreenPoint(Camera.main, trs3D.position);
		Open((int)Pos2d.x, (int)Pos2d.y, offsetX, offsetY);
	}


	public void Close() {

		ieOpenWindow = IEClosedWindow(OpenTime);
		StartCoroutine(ieOpenWindow);
	}


	// Use this for initialization
	void Awake () {

		BaseSize = rectTrs.sizeDelta;
		StartSize = BaseSize * 0.01f;

		rectTrs.sizeDelta = StartSize;
		gameObject.SetActive(false);
	}
	

	//========================================================================================
	//                                    private
	//========================================================================================

	// 基本サイズ
	private Vector2 BaseSize;

	private Vector2 StartSize;


	RectTransform _rectTrs;
	public RectTransform rectTrs {
		get {
			if (_rectTrs == null) {
				_rectTrs = GetComponent<RectTransform>();
			}
			return _rectTrs;
		}
	}

	IEnumerator ieOpenWindow;
	
	IEnumerator IEOpenedWindow(float MaxTime) {

		float time = 0f;
		rectTrs.sizeDelta = StartSize;

		while (true) {

			time += Time.deltaTime;

			if (time >= MaxTime) {
				break;
			}

			float rate = time / MaxTime;
			float x = BaseSize.x * CurveX.Evaluate(rate);
			float y = BaseSize.y * CurveY.Evaluate(rate);
			rectTrs.sizeDelta = new Vector2(x, y);

			yield return null;
		}

		rectTrs.sizeDelta = BaseSize;
	}

	IEnumerator IEClosedWindow(float MaxTime) {

		float time = 0f;
		rectTrs.sizeDelta = BaseSize;

		while (true) {

			time += Time.deltaTime;

			if (time >= MaxTime) {
				break;
			}

			float rate = 1f - (time / MaxTime);
			float x = BaseSize.x * CurveX.Evaluate(rate);
			float y = BaseSize.y * CurveY.Evaluate(rate);
			rectTrs.sizeDelta = new Vector2(x, y);

			yield return null;
		}

		rectTrs.sizeDelta = StartSize;
		gameObject.SetActive(false);
	}
}
