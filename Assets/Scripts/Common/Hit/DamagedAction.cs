using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedAction : MonoBehaviour {



	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// ノックバック
	/// </summary>
	/// <param name="impact"></param>
	public void KnockBack(Vector3 impact ,float Length ,AnimationCurve Curve,float Time = 0.5f) {

		if (ienumMethod != null) {

			// 止める
			StopCoroutine(ienumMethod);
		}

		// 新しいものに差し替え
		Vector3 moved = NormalizeWithoutY(impact) * Length;
		ienumMethod = IEMovePosition(moved, Curve, Time);
		StartCoroutine(ienumMethod);
	}

	/// <summary>
	/// 吸引
	/// </summary>
	/// <param name="centerPos"></param>
	/// <param name="length"></param>
	public void Suction(Vector3 centerPos ,float rate, AnimationCurve Curve, float Time = 0.5f) {

		if (ienumMethod != null) {

			// 止める
			StopCoroutine(ienumMethod);
		}

		// 新しいものに差し替え
		Vector3 moved = (centerPos - transform.position);
		moved.y = 0f;
		moved *= Mathf.Clamp01(rate);
		ienumMethod = IEMovePosition(moved, Curve,Time);
		StartCoroutine(ienumMethod);
	}

	static public Vector3 NormalizeWithoutY(Vector3 v) {
		v.y = 0f;
		return v.normalized;
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	IEnumerator IEMovePosition(Vector3 Moved, AnimationCurve Curve, float MaxTime) {

		Transform trs = transform;
		Vector3 pos = trs.position;
		float time = 0f;

		while (true) {

			time += Time.deltaTime;
			float rate = time / MaxTime;

			if (rate >= 1.0f) {
				break;
			}

			rate = Curve.Evaluate(rate);
			trs.position = pos + Moved * rate;

			yield return null;
		}

		trs.position = pos + Moved;
	}

	IEnumerator ienumMethod;
}
