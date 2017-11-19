using System.Collections;
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

    int Playercnt;

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
                           (Mathf.Abs(PlayersObject[0].transform.position.x - myPos.x) * Mathf.Abs(PlayersObject[0].transform.position.x - myPos.x)) +
                           (Mathf.Abs(PlayersObject[0].transform.position.z - myPos.z) * Mathf.Abs(PlayersObject[0].transform.position.z - myPos.z));
        float length;

        for (int i = 1;i < Playercnt;i++)
        {
            length = (Mathf.Abs(PlayersObject[i].transform.position.x - myPos.x) * Mathf.Abs(PlayersObject[0].transform.position.x - myPos.x)) +
                     (Mathf.Abs(PlayersObject[i].transform.position.x - myPos.x) * Mathf.Abs(PlayersObject[0].transform.position.x - myPos.x)) +
                     (Mathf.Abs(PlayersObject[i].transform.position.z - myPos.z) * Mathf.Abs(PlayersObject[i].transform.position.z - myPos.z));

            if (length < nearlength)
            {
                nearlength = length;
                resobj = PlayersObject[i];
            }
        }

        return resobj;
    }
}
