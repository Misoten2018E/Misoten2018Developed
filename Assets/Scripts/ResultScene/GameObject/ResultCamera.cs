using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCamera : MonoBehaviour {

    Animator ani;
    float nowtime;

    const float MAX_TIME = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResultCameraInit()
    {
        ani = GetComponent<Animator>();

        ani.enabled = false;
        nowtime = 0;
    }

    public bool ResultCameraUpdate()
    {
        bool flg = false;
        ani.enabled = true;

        nowtime += Time.deltaTime;
        if (nowtime >= MAX_TIME)
        {
            flg = true;
        }

        return flg;
    }
}
