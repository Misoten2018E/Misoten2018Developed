using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : SceneStartEvent{

    int score;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int ten)
    {
        score += ten;
        print("スコア" + score);
    }

    public int GetScore()
    {
        return score;
    }

    static Score _instance;
    static public Score instance
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
}
