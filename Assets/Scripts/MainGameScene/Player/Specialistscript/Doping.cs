using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doping : SceneStartEvent{

    const float DOPINGUP = 1.5f;
    float totaltime;
    float maxtime = 10;
    bool initflg = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!initflg) return;

        totaltime += Time.deltaTime;
        if (totaltime > maxtime)
        {
            PlayerBase PB = transform.parent.GetComponent<PlayerBase>();
            PB.AttackDOWN(DOPINGUP);
            Destroy(gameObject);
        }
    }

    public void DopingInit()
    {
        PlayerBase PB = transform.parent.GetComponent<PlayerBase>();
        PB.AttackUP(DOPINGUP);
        totaltime = 0;
        initflg = true;
    }
}
