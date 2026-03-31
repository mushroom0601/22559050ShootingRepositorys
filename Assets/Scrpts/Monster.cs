using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float spd = 5.0f;
    Vector3 direct = Vector3.down;

    private void Update()
    {
        transform.position = transform.position + direct * spd * Time.deltaTime;
    }
  
}
