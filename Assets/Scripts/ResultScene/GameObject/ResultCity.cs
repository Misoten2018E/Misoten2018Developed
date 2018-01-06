using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCity : MonoBehaviour {
    public List<int> ScoreList;
    public int CityModelCnt;

    Animator[] AnimatorArray;
    int[] CityStaArray;
    int MAXLv;
    int NOWLv;

    float nowtime;

    const float MAX_TIME = 2.0f;
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
        MAXLv = SceneToSceneDataSharing.Instance.mainToResultData.CityLevel;
        print("MAXLv"+ MAXLv);
        nowtime = 0;
        NOWLv = -1;
    }

    public bool ResultCityUpdate()
    {
        bool flg = false;

        nowtime += Time.deltaTime;

        if (nowtime > MAX_TIME)
        {
            NOWLv += 1;
            print("NOWLv"+ NOWLv);
            if (NOWLv >= MAXLv)
            {
                flg = true;
            }
            else
            {
                AnimatorArray[NOWLv].SetInteger("CitySta",1);
                nowtime = 0;
            }
            
        }


        return flg;
    }
}
