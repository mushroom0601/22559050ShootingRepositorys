using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI nowScoreUI;
    public TextMeshProUGUI bestScoreUI;

    public int nowScore;
    public int bestScore;

    public int normalKillCount;      // 일반 몬스터 처치 수
    public bool bossSpawnRequested;  // 보스 등장 요청

    void Start()
    {
        nowScore = 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        //  반드시 초기화
        normalKillCount = 0;
        bossSpawnRequested = false;

        Debug.Log(" ScoreManager 초기화 완료");

        RefreshUI();
    }

    public void AddScore(int amount)
    {
        nowScore += amount;

        if (nowScore > bestScore)
        {
            bestScore = nowScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        RefreshUI();
    }

    public void AddNormalKill()
    {
        normalKillCount++;

        Debug.Log($" 킬 증가: {normalKillCount}");

        //  5킬마다 보스 요청
        if (normalKillCount % 5 == 0)
        {
            bossSpawnRequested = true;
            Debug.Log(" 보스 요청 활성화됨");
        }
    }

    public void BossSpawnConsumed()
    {
        bossSpawnRequested = false;
        Debug.Log(" 보스 요청 소비됨");
    }

    private void RefreshUI()
    {
        if (nowScoreUI != null)
            nowScoreUI.text = "Now Score : " + nowScore;

        if (bestScoreUI != null)
            bestScoreUI.text = "Best Score : " + bestScore;
    }
}