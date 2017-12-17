using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackObjectManager : MonoBehaviour {


	//========================================================================================
	//                                    inspector
	//========================================================================================


	//========================================================================================
	//                                     public
	//========================================================================================

	public void SetAttackState(BossAttackObject.BodyType type ,bool isActive) {

		if (isActive) {

			for (int i = 0; i < AttackList.Count; i++) {

				if (AttackList[i].TypeCheck(type)) {
					AttackList[i].Activate();
				}
			}
		}
		else {

			for (int i = 0; i < AttackList.Count; i++) {

				if (AttackList[i].TypeCheck(type)) {
					AttackList[i].Disable();
				}
			}
		}
	}

	public void AttackStateOff() {

		for (int i = 0; i < AttackList.Count; i++) {
			AttackList[i].Disable();
		}
	}
	
	//========================================================================================
	//                                    override
	//========================================================================================
	
		// Use this for initialization
	void Start() {

		var a = GetComponentsInChildren<BossAttackObject>();
		AttackList.AddRange(a);

		for (int i = 0; i < AttackList.Count; i++) {

			AttackList[i].Disable();
		}
	}

	//========================================================================================
	//                                     private
	//========================================================================================


	List<BossAttackObject> AttackList = new List<BossAttackObject>();

}
