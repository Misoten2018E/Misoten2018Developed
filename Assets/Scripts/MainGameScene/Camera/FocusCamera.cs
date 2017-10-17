using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private List<Transform> TargetTransform;


	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var c = Camera.main;
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	

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

	
	public struct Square {
		public Vector2 xzPlus, xzMinus;
	}
}
