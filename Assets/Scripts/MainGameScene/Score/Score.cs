using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : SceneStartEvent{


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("移動するだけの敵スコア")]
	[SerializeField] private int EnemyFixedScore = 1;
	[Tooltip("プレイヤーを攻撃する敵スコア")]
	[SerializeField] private int EnemyAttackScore = 1;
	[Tooltip("中型の敵スコア")]
	[SerializeField] private int EnemyMediumScore = 10;
	[Tooltip("ボスからポップする敵スコア")]
	[SerializeField] private int EnemyBossPopScore = 5;


	//========================================================================================
	//                                     public
	//========================================================================================

	public enum ScoreType {

		E_Fixed,
		E_Attack,
		E_Medium,
		E_BossPop,

	}

	//========================================================================================
	//                                     private
	//========================================================================================



	int score;


	int debugId = -1;
	public void AddScore(int ten) {
		score += ten;
		debugId = DebugLog.ChaseLog("現在スコア : " + score, debugId);
	}

	public void AddScore(ScoreType type) {

		switch (type) {
			case ScoreType.E_Fixed:
				AddScore(EnemyFixedScore);
				break;
			case ScoreType.E_Attack:
				AddScore(EnemyAttackScore);
				break;
			case ScoreType.E_Medium:
				AddScore(EnemyMediumScore);
				break;
			case ScoreType.E_BossPop:
				AddScore(EnemyBossPopScore);
				break;
			default:
				break;
		}
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
