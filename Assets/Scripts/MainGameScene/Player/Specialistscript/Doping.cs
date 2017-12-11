using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doping : SceneStartEvent{

    const float DOPINGUP = 1.5f;
    float totaltime;
    const float maxtime = 10;
    bool initflg = false;
    int Charactersta;
    Player P;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!initflg) return;

        if (Charactersta != P.GetCharacterSta())
        {
            Destroy(gameObject);
            return;
        }

        totaltime += Time.deltaTime;
        if (totaltime > maxtime)
        {
            P.AttackDOWN(DOPINGUP);
            Destroy(gameObject);
        }
    }

    public void DopingInit()
    {
        P = transform.parent.GetComponent<Player>();
        P.AttackUP(DOPINGUP);
        totaltime = 0;
        Charactersta = P.GetCharacterSta();

        initflg = true;
    }

}
