using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedDamage : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("衝撃による揺れ動き方")]
	[SerializeField] private AnimationCurve ImpactTremble;

	[Range(0f,1f)]
	[SerializeField] private float TremblePower = 0.2f;

	//========================================================================================
	//                                    public
	//========================================================================================

	// Use this for initialization
	void Start () {
		
	}
	float t = 0f;
	// Update is called once per frame
	void Update () {

		t += Time.deltaTime;

		if (t > 3f) {

			HittedTremble(transform, transform.forward);
			t = 0f;
		}

	}

	/// <summary>
	/// モデルのトランスフォームを渡すこと
	/// 親を渡すと実際の座標などまで揺れるため
	/// </summary>
	/// <param name="trs"></param>
	/// <param name="impact"></param>
	public void HittedTremble(Transform trs ,Vector3 impact) {

		if (ieTrembled != null) {

			// 止める
			StopCoroutine(ieTrembled);
			
			// 初期座標に戻す
			TrembledTrs.localPosition = TrembleStartPosition;	
		}

		// 今回の初期化
		TrembledTrs = trs;
		TrembleStartPosition = trs.localPosition;

		// 新しいものに差し替え
		ieTrembled = IETremble(impact);
		StartCoroutine(ieTrembled);
	}



	//========================================================================================
	//                                    private
	//========================================================================================

	/// <summary>
	/// 揺れ動き
	/// </summary>
	/// <param name="trs"></param>
	/// <param name="impact"></param>
	/// <returns></returns>
	IEnumerator IETremble(Vector3 impact) {

		const float MaxTime = 0.2f;
		float time = 0f;

		impact = impact * TremblePower;

		while (true) {

			time += Time.deltaTime;

			if (time >= MaxTime) {
				break;
			}

			float rate = ImpactTremble.Evaluate(time / MaxTime);
			TrembledTrs.localPosition = TrembleStartPosition + impact * rate;

			yield return null;
		}

		TrembledTrs.localPosition = TrembleStartPosition;
		ieTrembled = null;
	}

	/// <summary>
	/// 初期座標保持
	/// </summary>
	private Vector3 TrembleStartPosition;
	private Transform TrembledTrs;

	IEnumerator ieTrembled;
}
