using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float spd = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 화면 위쪽(Y+)으로 발사
        transform.Translate(Vector3.up * spd * Time.deltaTime, Space.World);
    }
}