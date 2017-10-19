using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スラープ管理クラス
/// </summary>
public class QuaternionComplession {



	//========================================================================================
	//                                    public
	//========================================================================================

	public void Initialize(Quaternion start,Quaternion end) {
		StartQt = start;
		EndQt = end;
	}


	public Quaternion Slerp(float rate) {

		// 0～1の確認
		rate = Mathf.Clamp01(rate);

		return Quaternion.Slerp(StartQt, EndQt, rate);
	}


	/// <summary>
	/// 回転速度の限界を考慮した回転計算
	/// (中でTime.deltaTimeしてます)
	/// </summary>
	/// <returns></returns>
	public Quaternion RotateLimit(Quaternion NowQt, float RotateLimitDegree) {

		float limit = RotateLimitDegree * Time.deltaTime;

		// 回転しないといけない角度
		float targetAngle = Quaternion.Angle(NowQt, EndQt);

		// 目標角度より限界値が低い場合
		if (targetAngle > limit) {

			// 比率計算を行い、qtを返す
			float rate = limit / targetAngle;
			return Quaternion.Slerp(NowQt, EndQt, rate);
		}
		else {
			return EndQt;
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	private Quaternion startQt;
	public Quaternion StartQt {
		get {
			return startQt;
		}
		private set {
			startQt = value;
		}
	}

	private Quaternion endQt;
	public Quaternion EndQt {
		get {
			return endQt;
		}
		private set {
			endQt = value;
		}
	}


}

/// <summary>
/// Vector補完計算
/// </summary>
public class Vector3Complession{


	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// 初期位置(これだけ変更したい時用)
	/// </summary>
	public Vector3 Start {
		set {
			StartPosition = value;
			LengthVector = EndPosition - StartPosition;
		}
	}

	/// <summary>
	/// 終了位置(これだけ変更したい時用)
	/// </summary>
	public Vector3 End {
		set {
			EndPosition = value;
			LengthVector = EndPosition - StartPosition;
		}
	}

	public void Initialize(Vector3 start,Vector3 end) {
		StartPosition = start;
		EndPosition = end;

		LengthVector = EndPosition - StartPosition;
	}

	/// <summary>
	/// 現在補完位置計算
	/// </summary>
	/// <param name="rate"></param>
	/// <returns></returns>
	public Vector3 CalcPosition(float rate) {

		// 0～1の確認
		rate = Mathf.Clamp01(rate);

		Vector3 now = StartPosition + LengthVector * rate;
		return now;
	}

	/// <summary>
	/// 移動速度の限界を考慮した移動計算
	/// (中でTime.deltaTimeしてます)
	/// <para>辿りついたらtrueで、MovePosに最終地が返される</para>
	/// </summary>
	/// <returns></returns>
	public bool MoveLimit(Vector3 NowPos, float MoveLimitLength ,out Vector3 MovePos) {

		float timeLen = MoveLimitLength * Time.deltaTime;
		float limit = (timeLen * timeLen);

		// 移動しないといけない量
		Vector3 Move = (EndPosition - NowPos);
		float length = Move.magnitude;

		// 目標への移動量より限界値が低い場合
		if (length*length > limit) {

			// 比率計算を行い、移動量を返す
			float rate = (timeLen / length);
			Vector3 move = Move * rate;
			MovePos = move;
			return false;
		}
		else {
			MovePos = EndPosition;
			return true;
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	Vector3 StartPosition;
	Vector3 EndPosition;

	Vector3 LengthVector;
}

/// <summary>
/// Vector補完計算
/// </summary>
public class floatComplession {


	//========================================================================================
	//                                    public
	//========================================================================================

	/// <summary>
	/// 初期位置(これだけ変更したい時用)
	/// </summary>
	public float Start {
		set {
			start = value;
			Length = end - start;
		}
		get { return start; }
	}

	/// <summary>
	/// 終了位置(これだけ変更したい時用)
	/// </summary>
	public float End {
		set {
			end = value;
			Length = end - start;
		}
		get { return end; }
	}

	public void Initialize(float _start, float _end) {
		start = _start;
		end = _end;

		Length = end - start;
	}

	/// <summary>
	/// 現在補完位置計算
	/// </summary>
	/// <param name="rate"></param>
	/// <returns></returns>
	public float CalcFloat(float rate) {

		// 0～1の確認
		rate = Mathf.Clamp01(rate);

		float now = Start + Length * rate;
		return now;
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	float start;
	float end;

	float Length;
}
