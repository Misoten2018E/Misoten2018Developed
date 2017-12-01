using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckProducePhotoCamera : IFGameEndEvent {

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

		var tex = PhotoCamera.Photo(TexWidth, TexHeight);
		var list = PhotoShotList[playerSta];
		list[(int)type].photo = tex;
		list[(int)type].isShootted = true;
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
	void Start() {
		
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
	}

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

	private void CalcCameraPosition() {

		// 位置計算


		// 方向計算


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
