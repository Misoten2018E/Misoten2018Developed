using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : SceneStartEvent{

    public List<int> ScoreList;
    public int CityModelCnt;

    Animator[] AnimatorArray;
    int[] CityStaArray;
    int score;


	private void Awake() {

		myInstance = this;

	}

	// Use this for initialization
	void Start() {

	}

	int debugId = -1;

	// Update is called once per frame
	void Update() {
        if (!isInitialized) return;

		score = Score.instance.GetScore();
		debugId = DebugLog.ChaseLog("city" + score, debugId);
		
        for (int i = 0;i < ScoreList.Count ;i++)
        {
            if (ScoreList[i] <= score)
            {
                CityStaArray[i] = 1;
				CityLevel = i;

			}
            else
            {
                break;
            }
        }

        for (int i = 0;i < CityModelCnt;i++)
        {
            AnimatorArray[i].SetInteger("CitySta", CityStaArray[i]);
        }
    }

    public override void SceneMyInit()
    {
        AnimatorArray = new Animator[CityModelCnt];
        CityStaArray = new int[CityModelCnt];

        AnimatorArray[0] = transform.Find("city1").GetComponent<Animator>();
        AnimatorArray[1] = transform.Find("city2").GetComponent<Animator>();
        AnimatorArray[2] = transform.Find("city3").GetComponent<Animator>();

        for (int i = 0;i <  CityModelCnt;i++)
        {
            CityStaArray[i] = 0;
        }
        score = 0;

        isInitialized = true;
    }


    // シングルトンインスタンス
    static City myInstance;
    static public City Instance
    {
        get
        {
            return myInstance;
        }
    }


	int _CityLevel;
	public int CityLevel {
		private set { _CityLevel = value; }
		get { return _CityLevel; }
	}
      
}
