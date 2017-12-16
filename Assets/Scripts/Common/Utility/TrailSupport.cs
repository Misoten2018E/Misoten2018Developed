﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[DisallowMultipleComponent]
public class TrailSupport : MonoBehaviour {

	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private BodyType bodyType;


	//========================================================================================
	//                                     public
	//========================================================================================

	public void StartTrail() {

		TrailRen.enabled = true;
	}

	public void EndTrail() {

		TrailRen.enabled = false;
	}

	TrailRenderer _TrailRen;
	public TrailRenderer TrailRen {
		get {
			if (_TrailRen == null) {
				_TrailRen = GetComponent<TrailRenderer>();
			}
			return _TrailRen;
		}
	}

	public enum BodyType {
		LeftArm,
		RightArm,
		LeftLeg,
		RightLeg
	}


	public BodyType MyBodyType {
		private set { bodyType = value; }
		get { return bodyType; }
	}
      

	//========================================================================================
	//                                 public - override
	//========================================================================================

	// Use this for initialization
	void Start() {
		var t = TrailRen;
		EndTrail();
	}


	//========================================================================================
	//                                     private
	//========================================================================================

	
}
