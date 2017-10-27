using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectBlowOff : HitObject {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float BlowOffLength = 1f;

	//========================================================================================
	//                                    public - override
	//========================================================================================


	// Use this for initialization
	void Start () {
		hitType = HitType.BlowOff;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//========================================================================================
	//                                    public
	//========================================================================================

	public float Moved {
		get { return BlowOffLength; }
	}
}
