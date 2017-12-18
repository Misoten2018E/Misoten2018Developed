using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGameObjManager : MonoBehaviour {

    enum RESULT_STA
    {
        CITYMODE,
        PLAYERMODE,
        NEXTSCENE,
        RESULT_STA_MAX
    }

    const float MAX_TIME = 2.0f;

    RESULT_STA resultsta;
    ResultCity Rcity;
    ResultCamera RCamera;
    float nowtime;

    // Use this for initialization
    void Start () {
        resultsta = RESULT_STA.CITYMODE;
        Rcity = GetComponentInChildren<ResultCity>();
        RCamera = GetComponentInChildren<ResultCamera>();

        Rcity.ResultCityInit();
        RCamera.ResultCameraInit();
        nowtime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        bool res;

        switch (resultsta)
        {
            case RESULT_STA.CITYMODE:
                res = Rcity.ResultCityUpdate();

                if (res)
                {
                    resultsta = RESULT_STA.PLAYERMODE;
                }
                break;
            case RESULT_STA.PLAYERMODE:
                res = RCamera.ResultCameraUpdate();

                if (res)
                {
                    nowtime += Time.deltaTime;
                }

                if (nowtime >= MAX_TIME)
                {
                    resultsta = RESULT_STA.NEXTSCENE;
                }
                break;
            case RESULT_STA.NEXTSCENE:
                ResultManager.Instance.NextSceneStart();
                resultsta = RESULT_STA.RESULT_STA_MAX;
                break;
            case RESULT_STA.RESULT_STA_MAX:
                break;


        }
    }
}
