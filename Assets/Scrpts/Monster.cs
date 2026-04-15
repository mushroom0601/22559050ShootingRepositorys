using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float spd = 5.0f;
    Vector3 direct = Vector3.down;

    public GameObject prefabsExplosion;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = transform.position + direct * spd * Time.deltaTime;
    }

    /*OnCollisionEnter : 두 오브젝트가 닿았을 때, 딱 1번.
     * (Collision.collision): Collsion 상대방 객체 Collider
     */

  private void OnCollisionEnter(Collision collision)
    {
        GameObject explosionObj = Instantiate(prefabsExplosion);
        explosionObj.transform.position = transform.position;

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
