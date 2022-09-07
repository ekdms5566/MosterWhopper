using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float speed = 10f;
    RigidbodyConstraints defaultRbCon;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        defaultRbCon = rb.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 getVel = new Vector3(xMove, 0, zMove) * speed;
        rb.velocity = getVel;

        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraChasing>().isFPV)
            rb.constraints = RigidbodyConstraints.FreezeAll;
        else
            rb.constraints = defaultRbCon;

        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }
}
