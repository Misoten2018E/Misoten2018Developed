using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doping : SceneStartEvent{

    public float DopingATK;
    public float DopingSPE;
    float totaltime;
    public float maxtime;
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
            P.AttackDOWN(DopingATK, DopingSPE);
            Destroy(gameObject);
        }
    }

    public void DopingInit()
    {
        P = transform.parent.GetComponent<Player>();
        P.AttackUP(DopingATK, DopingSPE);
        totaltime = 0;
        Charactersta = P.GetCharacterSta();
        SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_SP_PowerUp, transform.position);

        initflg = true;
    }

}
