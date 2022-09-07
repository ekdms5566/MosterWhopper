using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    Vector3 TargetPos = new Vector3(0.9f, 1.6f, -3.36f); // 목표 위치
    Quaternion TargetRot = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f); // 목표 각도
    public float transSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * transSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, Time.deltaTime * transSpeed);
    }
}
