using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupLeaderEnemy : MoveFixedEnemy {

	// Use this for initialization
	protected override void Start () {
		
	}

	// Update is called once per frame
	protected override void Update () {
		
	}
}


public interface IFGroupEnemyCommand {

	void GroupAttack();

}