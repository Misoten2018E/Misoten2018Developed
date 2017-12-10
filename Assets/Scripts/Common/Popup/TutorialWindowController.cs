using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindowController : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private Sprite SetImage;
	[SerializeField] private Vector2 _ImageSize;

	[TextArea(1,3)]
	[SerializeField] private string Text;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum AnimType {
		Appear,
		Continuous,
		Disappear,
		DisappearCont,

	}

	public void Animation(AnimType type) {

		SetAnimation(type);
	}

	public string TextArea {
		set {
			Text = value;
			ChildText.text = Text;
		}
		get { return Text; }
	}

	public Sprite ImageArea {
		set {
			SetImage = value;
			
		}
		get { return SetImage; }
	}

	public Vector2 ImageSize {
		set { _ImageSize = value; }
		get { return _ImageSize; }
	}

	//========================================================================================
	//                                    override
	//========================================================================================


	//========================================================================================
	//                                     private
	//========================================================================================

	private const string TriggerStr = "Animation";
	private const string StateStr = "State";

	/// <summary>
	/// アニメーションのセット
	/// </summary>
	/// <param name="type"></param>
	private void SetAnimation(AnimType type) {

		MyAnim.SetInteger(StateStr, (int)type);
		MyAnim.SetTrigger(TriggerStr);
	}

	private void SetImageTexture(Sprite sprite) {

		var image = ChildIImages;

		// 1以下の場合背景しか存在しないので
		if (image.Length > 1) {
			image[1].sprite = sprite;
		}
	}

	private void SetImageSize(Vector2 size) {

		var image = ChildIImages;

		// 1以下の場合背景しか存在しないので
		if (image.Length > 1) {
			image[1].rectTransform.sizeDelta = size;
		}
	}

	Animator _MyAnim;
	protected Animator MyAnim {
		get {
			if (_MyAnim == null) {
				_MyAnim = GetComponent<Animator>();
			}
			return _MyAnim;
		}
	}


	Text _ChildText;
	public Text ChildText {
		get {
			if (_ChildText == null) {
				_ChildText = GetComponentInChildren<Text>();
			}
			return _ChildText;
		}
	}



	Image[] _ChildIImage;
	public Image[] ChildIImages {
		get {
			if (_ChildIImage == null) {
				_ChildIImage = GetComponentsInChildren<Image>();
			}
			return _ChildIImage;
		}
	} 
}
