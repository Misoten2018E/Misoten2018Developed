using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class testActionBase : MonoBehaviour {

	public abstract void Initialize(testPlayer player);
	public abstract void Destruct();

	public abstract void ActionCircle();
	public abstract void ActionCross();
	public abstract void ActionSquare();
	public abstract void ActionTriangle();
}
