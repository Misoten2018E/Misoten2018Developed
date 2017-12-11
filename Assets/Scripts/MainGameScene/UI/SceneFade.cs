using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : SceneStartEvent {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] bool AwakeFadeIn = false;

	[SerializeField] FadeType FadeInType = FadeType.Characters;
	[SerializeField] FadeType FadeOutType = FadeType.Characters;

	[SerializeField] Transform FadeInObj;
	[SerializeField] Transform FadeOutObj;
	[SerializeField] Transform NormalFade;
		
	//========================================================================================
	//                                     public
	//========================================================================================

	/// <summary>
	/// フェードイン
	/// </summary>
	public void FadeIn(System.Action endCall = null) {

		switch (FadeInType) {
			case FadeType.Normal:
				NormalFadeIn();
				break;
			case FadeType.Characters:
				CharacterFadeIn();
				break;
			default:
				break;
		}

		EndCallBack += endCall;
	}


	/// <summary>
	/// フェードアウト
	/// </summary>
	public void FadeOut(System.Action endCall = null) {

		switch (FadeOutType) {
			case FadeType.Normal:
				NormalFadeOut();
				break;
			case FadeType.Characters:
				CharacterFadeOut();
				break;
			default:
				break;
		}

		EndCallBack += endCall;
	}


	bool isFadeEnd = false;
	public bool IsFadeEnd {
		private set { isFadeEnd = value; }
		get { return isFadeEnd; }
	}

	/// <summary>
	/// フェード終了
	/// </summary>
	public void FadeAnimationEnd() {
		IsFadeEnd = true;

		if (EndCallBack != null) {
			EndCallBack();
			EndCallBack = null ;
		}
	}

	public enum FadeType {

		Normal,
		Characters
	}
	
	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		IsFadeEnd = true;

		// 起動時フェード
		if (AwakeFadeIn) {
			FadeIn();
		}

	}



	//========================================================================================
	//                                     private
	//========================================================================================

	void CharacterFadeIn() {

		gameObject.SetActive(true);
		IsFadeEnd = false;
		FadeOutObj.gameObject.SetActive(false);
		NormalFade.gameObject.SetActive(false);
		AnimationCharacterFadeIn();
	}
	
	void CharacterFadeOut() {

		gameObject.SetActive(true);
		IsFadeEnd = false;
		FadeInObj.gameObject.SetActive(false);
		NormalFade.gameObject.SetActive(false);
		AnimationCharacterFadeOut();
	}

	void NormalFadeIn() {

		gameObject.SetActive(true);
		IsFadeEnd = false;
		FadeOutObj.gameObject.SetActive(false);
		FadeInObj.gameObject.SetActive(false);
		AnimationNormalFadeIn();
	}

	void NormalFadeOut() {

		gameObject.SetActive(true);
		IsFadeEnd = false;
		FadeInObj.gameObject.SetActive(false);
		FadeOutObj.gameObject.SetActive(false);
		AnimationNormalFadeOut();
	}




	const string fadeInStr = "FadeIn";
	const string fadeOutStr = "FadeOut";
	const string NormalfadeInStr = "NormalFadeIn";
	const string NormalfadeOutStr = "NormalFadeOut";

	/// <summary>
	/// キャラクター型フェードイン
	/// </summary>
	void AnimationCharacterFadeIn() {

		Anim.SetTrigger(fadeInStr);

		EndCallBack = () => { FadeInObj.gameObject.SetActive(false); };
	}

	/// <summary>
	/// キャラクター型フェードアウト
	/// </summary>
	void AnimationCharacterFadeOut() {

		Anim.SetTrigger(fadeOutStr);
	}

	/// <summary>
	/// キャラクター型フェードイン
	/// </summary>
	void AnimationNormalFadeIn() {

		Anim.SetTrigger(NormalfadeInStr);

		EndCallBack = () => { NormalFade.gameObject.SetActive(false); };
	}

	/// <summary>
	/// キャラクター型フェードアウト
	/// </summary>
	void AnimationNormalFadeOut() {

		Anim.SetTrigger(NormalfadeOutStr);
	}

	System.Action EndCallBack;


	Animator _Anim;
	Animator Anim {
		get {
			if (_Anim == null) {
				_Anim = GetComponent<Animator>();
			}
			return _Anim;
		}
	}
      
}
