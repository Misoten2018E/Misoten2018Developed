using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

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
