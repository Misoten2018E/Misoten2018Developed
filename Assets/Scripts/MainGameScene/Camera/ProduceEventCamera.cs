using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceEventCamera : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private CameraEndCall EventAnimator;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum CameraEvent {
		BossPop,
		BossDestroy,
	}

	public void EventCameraStart(CameraEvent type, Transform target, Vector3 OptionPos, Vector3 optionRot) {

		SetBasePosition(target.position + OptionPos);
		SetBaseRotation(target.rotation * Quaternion.Euler(optionRot));

		AnimationStart(type);
	}

	public void EventCameraStart(CameraEvent type, Transform target, Vector3 OptionPos) {

		SetBasePosition(target.position + OptionPos);

		UINoticeManager.Instance.ActiveChangeIcons(false);

		EndCallback += () => { UINoticeManager.Instance.ActiveChangeIcons(true); };

		AnimationStart(type);
	}

	public void EventCameraStartWithRot(CameraEvent type, Transform target, Vector3 optionPos, Vector3 optionRot) {

		SetBasePosition(target.position + optionPos);
		SetBaseRotation(target.rotation * Quaternion.Euler(optionRot));

		UINoticeManager.Instance.ActiveChangeIcons(false);

		EndCallback += () => { UINoticeManager.Instance.ActiveChangeIcons(true); };

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
	//                                    override
	//========================================================================================

	private void Awake() {

		EventAnimator.enabled = false;
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	private const string StateStr = "State";
	private const string ActivateStr = "isActivate";

	private void AnimationStart(CameraEvent type) {

		// 最高優先度に
		MyCamera.depth = 5f;

		EventAnimator.EndCallBack += AnimationEnd;

		EventAnimator.MyAnim.SetInteger(StateStr, (int)type);
		EventAnimator.MyAnim.SetTrigger(ActivateStr);

		gameObject.SetActive(true);
		EventAnimator.enabled = true;
		//AnimatorStateInfo stateInfo = EventAnimator.MyAnim.GetCurrentAnimatorStateInfo(0);
		//EventAnimator.MyAnim.Play(stateInfo.fullPathHash, 0, 0.0f);  //初期位置に戻す

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
