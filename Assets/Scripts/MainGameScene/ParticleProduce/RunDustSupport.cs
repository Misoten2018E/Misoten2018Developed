using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectBase))]
public class RunDustSupport : PauseSupport {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float MoveLength = 0.1f;
	
	//========================================================================================
	//                                     public
	//========================================================================================

	//========================================================================================
	//                                 public - override
	//========================================================================================

	//========================================================================================
	//                                     private
	//========================================================================================


	// Use this for initialization
	void Start () {
		Effect = GetComponent<EffectBase>();
		MoveLen = MoveLength;
	}
	
	// Update is called once per frame
	void Update () {

		float lenSqr = ((transform.position - OldPosition).sqrMagnitude);

		if (Effect.IsActive) {

			if (lenSqr < sqrMoveLen * Time.deltaTime) {
				Effect.StopGenerate();
			}
		}
		else {

			if (lenSqr >= sqrMoveLen * Time.deltaTime) {
				Effect.Play();
			}
		}

		OldPosition = transform.position;
	}

	Vector3 OldPosition;

	float sqrMoveLen;

	TimeComplession TimeCheck = new TimeComplession(1f);


	public float MoveLen {
		private set {
			MoveLength = value;
			sqrMoveLen = MoveLength * MoveLength;
		}
		get { return MoveLength; }
	}
      

	EffectBase _Effect;
	public EffectBase Effect {
		private set { _Effect = value; }
		get { return _Effect; }
	}
      
}
