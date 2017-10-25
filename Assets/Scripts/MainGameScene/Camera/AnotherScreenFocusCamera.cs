using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherScreenFocusCamera : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] PopupedSubCamera SubRender;
	
	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {

		offsetPos = transform.position;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (TargetTrans == null) {
			return;
		}

		transform.position = TargetTrans.position + offsetPos;
	}

	public void Focus(Transform trs ,float Time) {

		gameObject.SetActive(true);
		TargetTrans = trs;

		// まず画面内かどうか確認
		// 画面内なら大きく出す必要無し
		// 画面外ならそこに最も近い画面内位置にウィンドウ表示

		SubRender.Open(trs);

		ieEvent = IEEventEnd(Time);
		StartCoroutine(ieEvent);
	}


	//========================================================================================
	//                                    public
	//========================================================================================

	Vector3 offsetPos;
	
	Transform TargetTrans;

	IEnumerator ieEvent;

	IEnumerator IEEventEnd(float Maxtime) {

		yield return new WaitForSeconds(Maxtime);
		SubRender.Close();

		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
		ieEvent = null;
	}
}
