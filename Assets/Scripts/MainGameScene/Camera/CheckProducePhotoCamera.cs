using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckProducePhotoCamera : MonoBehaviour,IFGameEndEvent {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] int TexWidth, TexHeight;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum PhotoType {

		LightEnd,
		Strong,

	}

	const int PhotoTypeMax = (int)PhotoType.Strong + 1;
	
	/// <summary>
	/// 撮影チャンス時にこの関数をセットする
	/// </summary>
	public void PhotoChance(Transform player,Transform target ,PhotoType type ,int playerSta) {

		if (!CheckPhotoChance(type,playerSta)) {
			return;
		}

		CalcCameraPosition(player, target);

		var tex = PhotoCamera.Photo(TexWidth, TexHeight);
		var list = PhotoShotList[playerSta];
		list[(int)type].photo = tex;
		list[(int)type].isShootted = true;

#if UNITY_DEBUG
		DebugCaptureImages.SetImages(tex);
#endif

	}


	// シングルトンインスタンス
	static CheckProducePhotoCamera myInstance;
	static public CheckProducePhotoCamera Instance {
		get {
			return myInstance;
		}
	}



	//========================================================================================
	//                                    override
	//========================================================================================

	// Use this for initialization
	void Awake() {

		myInstance = this;

		int max = ConstPlayerSta.StatusMax; ;
		for (int i = 0; i < max; i++) {

			var list = new List<TextureData>();
			for (int j = 0; j < PhotoTypeMax; j++) {
				var tex = new TextureData {
					isShootted = false,
				};
				list.Add(tex);
			}
			PhotoShotList.Add(list);
		}

		gameObject.SetActive(false);
	}

	/// <summary>
	/// プレイヤー操作終了時関数
	/// </summary>
	public void GameEnd() {

		for (int i = 0; i < PhotoShotList.Count; i++) {

			var list = PhotoShotList[i];
			for (int j = 0; j < list.Count; j++) {

				if (list[j].isShootted) {
					PhotoList.Add(list[j].photo);
				}
			}
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================


	List<List<TextureData>> PhotoShotList = new List<List<TextureData>>();

	/// <summary>
	/// 撮影チェック
	/// </summary>
	/// <param name="type"></param>
	/// <param name="playerSta"></param>
	/// <returns></returns>
	private bool CheckPhotoChance(PhotoType type, int playerSta) {

		var list = PhotoShotList[playerSta];
		// 既に撮影済みならスルー
		return !(list[(int)type].isShootted);
	}

	private void CalcCameraPosition(Transform player, Transform target) {

		// 位置計算
		Vector3 doubleLange = player.position + target.position;
		Vector3 lookat = doubleLange / 2;
		Vector3 pos = doubleLange / 2;
		pos.y = pos.y * 2f;

		var PlForward = (player.position - pos).normalized + player.right;
		PlForward.Normalize();
		pos = pos - PlForward * 4.5f;

		// 最低高さ保証
		pos.y = pos.y < 2f ? 2f : pos.y;	

		DebugLog.log("カメラ想定位置" + pos);
		DebugLog.log("カメラの見ている位置 : " + lookat);

		// 方向計算

		Camera cam = PhotoCamera.MyCamera;
		cam.transform.position = pos;
		cam.transform.LookAt(lookat);
	}


	PhotographCamera _PhotoCamera;
	public PhotographCamera PhotoCamera {
		get {
			if (_PhotoCamera == null) {
				_PhotoCamera = CameraManager.Instance.PhotoCamera;
			}
			return _PhotoCamera;
		}
	}
      


	List<Texture2D> _PhotoList = new List<Texture2D>();
	public List<Texture2D> PhotoList {
		private set { _PhotoList = value; }
		get { return _PhotoList; }
	}

	public class TextureData {

		public Texture2D photo;
		public bool isShootted;
	}
}
