using UnityEngine;

public class Monster : MonoBehaviour
{
    public float spd = 2.5f;
    public GameObject prefabsExplosion;

    private void Update()
    {
        transform.position += Vector3.down * spd * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            return;

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            scoreManager.AddScore(1);
            scoreManager.AddNormalKill();
        }

        if (prefabsExplosion != null)
        {
            GameObject explosionObj = Instantiate(prefabsExplosion);
            explosionObj.transform.position = transform.position;
        }

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}