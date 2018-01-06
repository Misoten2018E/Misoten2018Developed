using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	MultiInput Minput;

	[SerializeField] Image deleteImage;

    // Use this for initialization
    void Start () {
        Minput = GetComponent<MultiInput>();
		MyAnim.gameObject.SetActive(false);
		NowState = -1;
    }
	
	// Update is called once per frame
	void Update () {

        if (Minput.GetAllButtonCircleTrigger() && NowState == -1)
        {
			MyAnim.gameObject.SetActive(true);
			SetAnimation(AnimType.Appear);
			NowState = 1;
		}

        if (Minput.GetButtonCircleTrigger() && NowState == 1)
        {
			SetAnimation(AnimType.Change);
			SoundManager.Instance.PlaySE(SoundManager.SEType.TitlePlayerOK,new Vector3(0,0,0));
			NowState = 2;

		//	StartCoroutine(GameObjectExtensions.DelayMethod(0.3f, () => { deleteImage.gameObject.SetActive(false); }));
		}

		//if (NowState == 0) {
		//	NowState = 1;
		//}
	}


	void SetAnimation(AnimType type) {

		MyAnim.SetInteger(StateStr, (int)type);
		MyAnim.SetTrigger(ActiveStr);
	}

	public enum AnimType {
		Appear,
		Change,
	}

	int NowState = 0;

	const string StateStr = "State";
	const string ActiveStr = "isActivate";

	Animator _MyAnim;
	public Animator MyAnim {
		get {
			if (_MyAnim == null) {
				_MyAnim = GetComponentInChildren<Animator>();
			}
			return _MyAnim;
		}
	} 
}
