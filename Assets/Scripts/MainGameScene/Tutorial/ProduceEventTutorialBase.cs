﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ProduceEventTutorialBase : ProduceEventBase{

	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private ProduceType EventProduce;

	//========================================================================================
	//                                     public
	//========================================================================================

	public enum ProduceType {

		Normal,
		Continuity,

	}

	//========================================================================================
	//                                    override
	//========================================================================================

	//========================================================================================
	//                                     private
	//========================================================================================

}