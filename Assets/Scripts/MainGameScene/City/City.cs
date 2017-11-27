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

	// Update is called once per frame
	void Update() {
        if (!isInitialized) return;

        score += 1;
        print("スコア" + score);

        for (int i = 0;i < ScoreList.Count ;i++)
        {
            if (ScoreList[i] <= score)
            {
                CityStaArray[i] = 1;
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
        AnimatorArray[3] = transform.Find("city4").GetComponent<Animator>();

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
}
