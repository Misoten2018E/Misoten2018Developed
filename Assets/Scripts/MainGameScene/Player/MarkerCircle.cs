using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCircle : SceneStartEvent{

    public int TargetNo;
    Transform tra;

	// Use this for initialization
	void Start () {
        tra = PlayerManager.instance.GetPlayerSearchNo(TargetNo).transform;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 v = new Vector3(tra.position.x, transform.position.y, tra.position.z);
        transform.position = v;
	}
}
