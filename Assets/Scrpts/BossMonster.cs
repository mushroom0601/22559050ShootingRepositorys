using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public float spd = 1.5f;
    public int maxHP = 3;
    private int currentHP;

    public GameObject prefabsExplosion;
    public MonsterManager monsterManager;

    public GameObject bossHPPrefab;
    private BossHPUI bossHPUI;
    private GameObject hpUIObject;

    private void Start()
    {
        currentHP = maxHP;

        if (bossHPPrefab != null)
        {
            // Canvas 아래에 생성하는 게 가장 안전함
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                hpUIObject = Instantiate(bossHPPrefab, canvas.transform);
            }
            else
            {
                hpUIObject = Instantiate(bossHPPrefab);
            }

            bossHPUI = hpUIObject.GetComponent<BossHPUI>();

            FollowWorldUI followUI = hpUIObject.GetComponent<FollowWorldUI>();
            if (followUI != null)
            {
                followUI.target = transform;
                followUI.targetCamera = Camera.main;
                followUI.worldOffset = new Vector3(0f, 1.2f, 0f);
            }

            if (bossHPUI != null)
            {
                bossHPUI.SetHP(currentHP, maxHP);
            }
        }
        else
        {
            Debug.LogWarning("bossHPPrefab이 연결되지 않았습니다.");
        }
    }

    private void Update()
    {
        transform.position += Vector3.down * spd * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            return;

        Destroy(collision.gameObject);

        currentHP--;

        if (bossHPUI != null)
        {
            bossHPUI.SetHP(currentHP, maxHP);
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            scoreManager.AddScore(3);
        }

        if (prefabsExplosion != null)
        {
            GameObject explosionObj = Instantiate(prefabsExplosion);
            explosionObj.transform.position = transform.position;
        }

        if (hpUIObject != null)
        {
            Destroy(hpUIObject);
        }

        if (monsterManager != null)
        {
            monsterManager.OnBossDead();
        }

        Destroy(gameObject);
    }
}
