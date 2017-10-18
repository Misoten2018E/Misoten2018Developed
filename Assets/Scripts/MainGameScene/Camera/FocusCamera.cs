using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FocusCamera : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private List<Transform> TargetTransform;

	[SerializeField] private float OrthoMinSize = 7;
	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {
		OffsetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		var mostFar = MostFarTransformPosition();

		OrthographicSizeUpdate(mostFar);

		transform.position = mostFar.Center3Dxz + OffsetPos;
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	// 初期カメラ位置の保持
	private Vector3 OffsetPos = new Vector3();

	/// <summary>
	/// カメラOrthographicサイズ更新
	/// </summary>
	/// <param name="mostFar"></param>
	private void OrthographicSizeUpdate(Square mostFar) {

		CameraComp.orthographicSize = CalcCameraOrthoSize(mostFar);

		if (CameraComp.orthographicSize < OrthoMinSize) {
			CameraComp.orthographicSize = OrthoMinSize;
		}
	}

	/// <summary>
	/// 中心点計算
	/// </summary>
	/// <returns></returns>
	Vector3 CalcTargetCenterPoint() {

		Vector3 pos = new Vector3();

		for (int i = 0; i < TargetTransform.Count; i++) {
			pos += TargetTransform[i].transform.position;
		}

		pos /= TargetTransform.Count;
		return pos;
	}

	/// <summary>
	/// 四方の最大位置検索
	/// </summary>
	/// <returns></returns>
	Square MostFarTransformPosition() {

		Square sq = new Square();

		for (int i = 0; i < TargetTransform.Count; i++) {

			var pos = TargetTransform[i].position;

			if (sq.xzPlus.x < pos.x) {
				sq.xzPlus.x = pos.x;
			}else if (sq.xzMinus.x > pos.x) {
				sq.xzMinus.x = pos.x;
			}

			if (sq.xzPlus.y < pos.z) {
				sq.xzPlus.y = pos.z;
			}
			else if (sq.xzMinus.y > pos.z) {
				sq.xzMinus.y = pos.z;
			}
		}
		return sq;
	}

	/// <summary>
	/// OrthoGraphicのサイズ計算
	/// </summary>
	/// <param name="sq"></param>
	/// <returns></returns>
	float CalcCameraOrthoSize(Square sq) {

		float lenX = sq.xzPlus.x - sq.xzMinus.x;
		float lenZ = sq.xzPlus.y - sq.xzMinus.y;

		return ((lenX > lenZ) ? (lenX) : (lenZ)) / 2;
	}

	

	Camera _CameraComp;
	public Camera CameraComp {
		get {
			if (_CameraComp == null) {
				_CameraComp = GetComponent <Camera>();
			}
			return _CameraComp;
		}
	}
      


	/// <summary>
	/// 受け渡し
	/// </summary>
	public struct Square {
		public Vector2 xzPlus, xzMinus;

		public Vector2 Center {
			get {
				return (xzPlus + xzMinus) / 2;
			}
		}
		
		public Vector3 Center3Dxz {
			get {
				var v = Center;
				return new Vector3(v.x, 0, v.y);
			}
		}
	}


}
