using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class HitSeriesofAction : PauseSupport {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("親に追従して動くかどうか")]
	[SerializeField] private bool isToFollowParent = true;

	//========================================================================================
	//                                    const
	//========================================================================================

	const string ANIM_ACCESS_STATUS = "isActivate";

	//========================================================================================
	//                                    public
	//========================================================================================

	// Use this for initialization
	void Start () {

		var a = Anim;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AnimationEnd() {

		if (EndCallback != null) {
			EndCallback();
		}

		gameObject.SetActive(false);
	}

	/// <summary>
	/// 初期処理
	/// </summary>
	/// <param name="player"></param>
	public void Initialize(testPlayer player) {
		myPlayer = player;

		var Hits = GetComponentsInChildren<HitObject>();
		for (int i = 0; i < Hits.Length; i++) {
			Hits[i].Initialize(this);
		}

		TransformInitialize(player.transform, isToFollowParent);

		EndCallback = null;
		gameObject.SetActive(false);
	}

	/// <summary>
	/// 親に追従するかどうかはセットする
	/// 親の正面に出る
	/// </summary>
	/// <param name="trs"></param>
	/// <param name="isChild"></param>
	public void TransformInitialize(Transform trs ,bool isChild) {

		// 親に追従するかどうか
		if (isChild) {
			transform.SetParent(trs);
			Vector3 buff = new Vector3(1, 1, 1);
			transform.localScale = buff;
			buff.Set(0, 0, 0);
			transform.localPosition = buff;
			transform.localRotation = Quaternion.identity;
		}
		else {
			transform.position = trs.position;
			transform.rotation = trs.rotation;
		}
	}

	/// <summary>
	/// 親に全く追従しない設置系
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="rot"></param>
	public void TransformInitialize(Vector3 pos ,Quaternion rot) {
		transform.position = pos;
		transform.rotation = rot;
	}

	public void SetEndCallback(System.Action act) {

		EndCallback += act;
	}

	/// <summary>
	/// 起動処理
	/// </summary>
	public void Activate() {

		gameObject.SetActive(true);
		AnimatorStateInfo stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
		Anim.Play(stateInfo.fullPathHash, 0, 0.0f);  //初期位置に戻す
	//	Anim.SetBool(ANIM_ACCESS_STATUS, true);
		
	}

	public bool isActive {
		get { return isActiveAndEnabled; }
	}

	//========================================================================================
	//                                    propety
	//========================================================================================

	Animator _Anim;
	public Animator Anim {
		get {
			if (_Anim == null) {
				_Anim = GetComponent<Animator>();
			}
			return _Anim;
		}
	}

	testPlayer _myPlayer;
	public testPlayer myPlayer {
		private set { _myPlayer = value; }
		get { return _myPlayer; }
	}
    
	public int PlayerNo {
		get { return myPlayer.no; }
	}


	System.Action EndCallback;
}
