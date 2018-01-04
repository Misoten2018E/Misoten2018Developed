using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopPosition : MonoBehaviour {

	[SerializeField] List<Transform> Positions;


	public List<Transform> PositionList {
		get { return Positions; }
	}
      
}
