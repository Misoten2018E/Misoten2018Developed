using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopPosition : MonoBehaviour {

	[SerializeField] List<Transform> Positions;


	List<Transform> _PositionList;
	public List<Transform> PositionList {
		private set { _PositionList = value; }
		get { return _PositionList; }
	}
      
}
