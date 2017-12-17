using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCity : MonoBehaviour {
    public List<int> ScoreList;
    public int CityModelCnt;

    Animator[] AnimatorArray;
    int[] CityStaArray;
    float score;

    float nowtime;

    const float MAX_TIME = 1.0f;
    const float FLAMEADDSCOER = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResultCityInit()
    {
        AnimatorArray = new Animator[CityModelCnt];
        CityStaArray = new int[CityModelCnt];

        AnimatorArray[0] = transform.Find("city1").GetComponent<Animator>();
        AnimatorArray[1] = transform.Find("city2").GetComponent<Animator>();
        AnimatorArray[2] = transform.Find("city3").GetComponent<Animator>();

        for (int i = 0; i < CityModelCnt; i++)
        {
            CityStaArray[i] = 0;
        }
        score = 0;
        nowtime = 0;
    }

    public bool ResultCityUpdate()
    {
        bool flg = false;
        if (score < Score.instance.GetScore())
        {
            score += FLAMEADDSCOER;
        }
        

        for (int i = 0; i < ScoreList.Count; i++)
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

        for (int i = 0; i < CityModelCnt; i++)
        {
            AnimatorArray[i].SetInteger("CitySta", CityStaArray[i]);
        }

        print("scoer"+ Score.instance.GetScore());
        if (score >=Score.instance.GetScore() || score >= ScoreList[CityModelCnt-1])
        {
            nowtime += Time.deltaTime;

            if (nowtime >= MAX_TIME)
            {
                flg = true;
            }
        }

        return flg;
    }
}
