using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PlayerBase{

    enum BOMB_STA
    {
        SET,
        SWITHON,
        BOMB_STAMAX
    }
    float time;
    BombHitAnimation bombhit;
    BOMB_STA bombsta;

    const int maxtime = 1;

	// Use this for initialization
	void Start () {
        bombsta = BOMB_STA.SET;
        time = 0;
        bombhit = GetComponent<BombHitAnimation>();
        bombhit.Initialize(this);
    }
	
	// Update is called once per frame
	void Update () {
        if (bombsta == BOMB_STA.SWITHON)
        {
            time += Time.deltaTime;
            
            if (time > maxtime)
            {
                Destroy(gameObject);
            }
        }	
	}

    public void InitBomb(GameObject Player)
    {
        myPlayer = Player;
    }

    public void BombswitchON(float atk)
    {
        bombhit.HitAnimationStrongattack(atk);
        bombsta = BOMB_STA.SWITHON;
    }

}
