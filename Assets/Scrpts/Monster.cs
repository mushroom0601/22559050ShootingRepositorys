using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float spd = 1.0f;

    public GameObject target;

    Vector3 direct = Vector3.down;

    private void Start()
    {
        int rndNum = Random.Range(0, 10);
        if(rndNum % 3 == 0)
        {
            direct = target.transform.position - transform.position;
            direct.Normalize();
        }
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
        /* 
         * Destroy : 유니티 객체 삭제 기본 상수
         * collision.gameObject : 상대방 객체
         */
        Destroy(collision.gameObject);

        // 내 객체 삭제
        Destroy(gameObject);
    }
}
