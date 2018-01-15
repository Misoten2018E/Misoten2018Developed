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
    Player p;
    int Settype;

    const float maxtime = 0.5f;

	// Use this for initialization
	void Start () {
        bombsta = BOMB_STA.SET;
        time = 0;
        bombhit = GetComponent<BombHitAnimation>();
        bombhit.Initialize(this);
    }
	
	// Update is called once per frame
	void Update () {
       
        if (Settype != p.GetCharacterSta())
        {
            Destroy(gameObject);
            return;
        }

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
        p = myPlayer.GetComponent<Player>();
        Settype = p.GetCharacterSta();
    }

    public void BombswitchON(float atk)
    {
        bombhit.HitAnimationStrongattack(atk);
        bombsta = BOMB_STA.SWITHON;
        SoundManager.Instance.PlaySE(SoundManager.SEType.Specialist_Strong_Exp, transform.position);

        var par = ResourceManager.Instance.Get<EffectBase>(ConstDirectry.DirParticle, ConstEffects.Explode);
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        EffectBase Effect = GameObject.Instantiate(par);
        EffectSupport.Follow(Effect, pos, transform.right * -1);
    }


	public override int ModelSta {
		get {
			return ConstPlayerSta.NormalCharacter;
		}
	}
}
