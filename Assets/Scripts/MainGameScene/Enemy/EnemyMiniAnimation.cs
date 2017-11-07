using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMiniAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	enum AnimationType {
		Move,
		AttackPose,
		Attack,
		Down,
		CityPose,
		RunAway
	}
}
