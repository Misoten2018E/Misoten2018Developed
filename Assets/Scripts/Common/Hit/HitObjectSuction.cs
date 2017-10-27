using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectSuction : HitObject {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float SuctionLength = 1f;

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	void Start () {
		hitType = HitType.Suction;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Sucion(DamagedAction action) {
		action.Suction(transform.position, Moved, MoveCurve);
	}

	//========================================================================================
	//                                    public
	//========================================================================================

	public float Moved {
		get { return SuctionLength; }
	}
}
