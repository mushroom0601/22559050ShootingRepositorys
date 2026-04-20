using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject prefabsMonster;
    public GameObject bossMonsterPrefab;

    public float minTime = 0.5f;
    public float maxTime = 1.5f;

    private float nowTime;
    private float createTime;

    private ScoreManager scoreManager;
    private bool bossAlive = false;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);
        scoreManager = FindObjectOfType<ScoreManager>();

        Debug.Log(" MonsterManager 시작됨");
    }

    void Update()
    {
        if (scoreManager == null)
        {
            Debug.LogError(" ScoreManager 없음");
            return;
        }

        //  현재 상태 확인 로그
        Debug.Log($"[상태] kill:{scoreManager.normalKillCount} | bossReq:{scoreManager.bossSpawnRequested} | bossAlive:{bossAlive}");

        //  보스 스폰 시도
        if (scoreManager.bossSpawnRequested && !bossAlive)
        {
            Debug.Log(" 보스 소환 시도");

            bool spawned = SpawnBoss();

            if (spawned)
            {
                Debug.Log(" 보스 생성 성공");

                scoreManager.BossSpawnConsumed();
                bossAlive = true;
            }
            else
            {
                Debug.LogError(" 보스 생성 실패 → 일반 몬스터 강제 생성");

                //  멈춤 방지 핵심
                SpawnNormalMonster();
            }

            return;
        }

        //  일반 몬스터 생성
        nowTime += Time.deltaTime;

        if (nowTime >= createTime && !bossAlive)
        {
            Debug.Log(" 일반 몬스터 생성");

            SpawnNormalMonster();
            createTime = Random.Range(minTime, maxTime);
            nowTime = 0f;
        }
    }

    void SpawnNormalMonster()
    {
        if (prefabsMonster == null)
        {
            Debug.LogError(" prefabsMonster 연결 안됨");
            return;
        }

        GameObject monster = Instantiate(prefabsMonster);

        float randomX = Random.Range(-3.5f, 3.5f);
        monster.transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
    }

    bool SpawnBoss()
    {
        if (bossMonsterPrefab == null)
        {
            Debug.LogError(" bossMonsterPrefab 연결 안됨");
            return false;
        }

        GameObject boss = Instantiate(bossMonsterPrefab);
        boss.transform.position = new Vector3(0f, transform.position.y, transform.position.z);

        BossMonster bossScript = boss.GetComponent<BossMonster>();

        if (bossScript == null)
        {
            Debug.LogError(" BossMonster 스크립트 없음");
            return false;
        }

        bossScript.monsterManager = this;

        return true;
    }

    public void OnBossDead()
    {
        Debug.Log(" 보스 죽음 → 일반 몬스터 재개");

        bossAlive = false;
    }
}