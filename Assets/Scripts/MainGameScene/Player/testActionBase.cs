using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class testActionBase : MonoBehaviour {


	public abstract void ActionCircle(GameObject player);
	public abstract void ActionCross(GameObject player);
	public abstract void ActionSquare(GameObject player);
	public abstract void ActionTriangle(GameObject player);

}
