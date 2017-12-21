using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelBarrett : PlayerBase {

    public float LiveMaxtime;
    public float BarrettSpeed;

    enum BARRETT_STA
    {
        LIVE,
        DETH,
        BARRETT_STAMAX
    }
    BARRETT_STA B_sta;
    float Livetime;
    bool initflg = false;

    // Use this for initialization
    void Start () {
        B_sta = BARRETT_STA.LIVE;
        Livetime = LiveMaxtime;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!initflg)
        {
            return;
        }

        switch (B_sta)
        {
            case BARRETT_STA.LIVE:
                Vector3 w_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                w_pos = w_pos + (transform.forward * BarrettSpeed * Time.deltaTime);
                
                transform.position = w_pos;

                Livetime -= Time.deltaTime;
                if (Livetime <= 0)
                {
                    B_sta = BARRETT_STA.DETH;
                }
                break;
            case BARRETT_STA.DETH:
                Destroy(gameObject);
                break;

        }
    }

    public void SetPlayerObj(GameObject myplayer)
    {
        HitSeriesofAction Hit;
        myPlayer = myplayer;
        Hit = GetComponentInChildren<HitSeriesofAction>();
        Hit.Initialize(this);
        Hit.Activate();

        initflg = true;
        
    }
}
