﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SceneStartEvent{

    static PlayerManager _instance;
    static public PlayerManager instance
    {
        private set
        {
            _instance = value;
        }
        get { return _instance; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;   
        }

    }

    public List<GameObject> PlayersObject;
    public GameObject Dopingobj;

    int Playercnt;

    readonly float HEEL_COS = Mathf.Cos(Mathf.PI/6);
    const float DOPINGAREA = 2;

    // Use this for initialization
    void Start () {
        Playercnt = PlayersObject.Count;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    //　HPが一番低いプレイヤーのオブジェクトを返す
    public GameObject GetLowHPPlayerObj(int myno)
    {
        GameObject resobj = PlayersObject[0];
        Player P;
        int minhp = 999;
        int hp;

        for (int i=0;i<Playercnt ;i++)
        {
            P = PlayersObject[i].gameObject.GetComponent<Player>();
            if (P.no != myno)
            {
                hp = P.GetPlayerHP();
                if (hp < minhp)
                {
                    minhp = hp;
                    resobj = PlayersObject[i];
                }
            }
            
        }

        return resobj;
    }

    //自分に一番近いプレイヤーのオブジェクトを返す
    public GameObject GetNearPlayerObj(Vector3 myPos)
    {
        GameObject resobj = PlayersObject[0];
        float nearlength = (Mathf.Abs(PlayersObject[0].transform.position.x - myPos.x) * Mathf.Abs(PlayersObject[0].transform.position.x - myPos.x)) +
                           (Mathf.Abs(PlayersObject[0].transform.position.y - myPos.y) * Mathf.Abs(PlayersObject[0].transform.position.y - myPos.y)) +
                           (Mathf.Abs(PlayersObject[0].transform.position.z - myPos.z) * Mathf.Abs(PlayersObject[0].transform.position.z - myPos.z));
        float length;

        for (int i = 1;i < Playercnt;i++)
        {
            length = (Mathf.Abs(PlayersObject[i].transform.position.x - myPos.x) * Mathf.Abs(PlayersObject[i].transform.position.x - myPos.x)) +
                     (Mathf.Abs(PlayersObject[i].transform.position.y - myPos.y) * Mathf.Abs(PlayersObject[i].transform.position.y - myPos.y)) +
                     (Mathf.Abs(PlayersObject[i].transform.position.z - myPos.z) * Mathf.Abs(PlayersObject[i].transform.position.z - myPos.z));

            if (length < nearlength)
            {
                nearlength = length;
                resobj = PlayersObject[i];
            }
        }

        return resobj;
    }

    public GameObject GetHeelSpecialObj(Vector3 front,Vector3 myPos,int myno)
    {
        GameObject resobj = null;
        float nearlength = 999999;
        float length;
        Vector3 v;
        float dot;

        for (int i = 0;i < Playercnt;i++)
        {
            Player P = PlayersObject[i].GetComponent<Player>();
            if (P.no == myno)
            {
                continue;
            }

            v = PlayersObject[i].transform.position - myPos;
            v.Normalize();
            dot = Vector3.Dot(front,v);

            if (dot > HEEL_COS)
            {
                length = (Mathf.Abs(PlayersObject[i].transform.position.x - myPos.x) * Mathf.Abs(PlayersObject[i].transform.position.x - myPos.x)) +
                         (Mathf.Abs(PlayersObject[i].transform.position.y - myPos.y) * Mathf.Abs(PlayersObject[i].transform.position.y - myPos.y)) +
                         (Mathf.Abs(PlayersObject[i].transform.position.z - myPos.z) * Mathf.Abs(PlayersObject[i].transform.position.z - myPos.z));
                
                if (length < nearlength)
                {
                    nearlength = length;
                    resobj = PlayersObject[i];
                }
            }
            
        }

        return resobj;
    }

    public void PlyerDoping(Vector3 mypos,int myno)
    {
        float length;
        GameObject obj;
        Vector3 objpos;

        for (int i = 0;i < Playercnt ;i++)
        {
            Player P = PlayersObject[i].GetComponent<Player>();
            if (P.no == myno)
            {
                continue;
            }

            length = (Mathf.Abs(PlayersObject[i].transform.position.x - mypos.x) * Mathf.Abs(PlayersObject[i].transform.position.x - mypos.x)) +
                         (Mathf.Abs(PlayersObject[i].transform.position.y - mypos.y) * Mathf.Abs(PlayersObject[i].transform.position.y - mypos.y)) +
                         (Mathf.Abs(PlayersObject[i].transform.position.z - mypos.z) * Mathf.Abs(PlayersObject[i].transform.position.z - mypos.z));


            if (DOPINGAREA * DOPINGAREA > length)
            {
                objpos = PlayersObject[i].transform.position;

                obj = Instantiate(Dopingobj, objpos, Quaternion.identity) as GameObject;
                obj.transform.parent = PlayersObject[i].transform;
                Doping doping = obj.GetComponent<Doping>();
                doping.DopingInit();
            }
        }
    }

    public GameObject GetPlayerSearchNo(int no)
    {
        return PlayersObject[no];
    }
}
