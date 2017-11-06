using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

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

    public GameObject GetLowHPPlayerObj(Vector3 myPos)
    {
        return PlayersObject[0];//Error出るから仮
    }
}
