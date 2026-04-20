using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float spd = 5f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 이 게임은 X, Y 평면에서 움직이는 구조
        Vector3 direct = new Vector3(h, v, 0f);

        transform.position += direct * spd * Time.deltaTime;
    }
}