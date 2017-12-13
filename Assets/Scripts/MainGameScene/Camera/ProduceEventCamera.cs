using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceEventCamera : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private Animator EventAnimator;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum CameraEvent {
		BossPop,

	}

	public void EventCameraStart(CameraEvent type, Transform target, Vector3 OptionPos, Vector3 optionRot) {

		SetBasePosition(target.position + OptionPos);
		SetBaseRotation(target.rotation * Quaternion.Euler(optionRot));

		AnimationStart(type);
	}

	public void EventCameraStart(CameraEvent type, Transform target, Vector3 OptionPos) {

		SetBasePosition(target.position + OptionPos);

		AnimationStart(type);
	}

	public void AnimationEnd() {

		if (EndCallback != null) {
			EndCallback();
			EndCallback = null;
		}

		// 優先度を初期値に
		MyCamera.depth = 0f;
	}


	System.Action _EndCallback;
	public System.Action EndCallback {
		set { _EndCallback = value; }
		get { return _EndCallback; }
	}
      

	//========================================================================================
	//                                     private
	//========================================================================================

	private void AnimationStart(CameraEvent type) {

		// 最高優先度に
		MyCamera.depth = 5f;
	}

	/// <summary>
	/// ベースとなる位置セット
	/// </summary>
	/// <param name="pos"></param>
	void SetBasePosition(Vector3 pos) {
		EventAnimator.transform.position = pos;
	}

	/// <summary>
	/// ベースとなる回転セット
	/// </summary>
	/// <param name="pos"></param>
	void SetBaseRotation(Quaternion rot) {
		EventAnimator.transform.rotation = rot;
	}


	Camera _MyCamera;
	private Camera MyCamera {
		get {
			if (_MyCamera == null) {
				_MyCamera = GetComponent<Camera>();
			}
			return _MyCamera;
		}
	}
      
}
