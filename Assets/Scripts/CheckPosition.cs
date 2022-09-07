using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    Vector3 lastPos;
    Animator customer;
    bool isWalk;
    // Start is called before the first frame update
    void Start()
    {
        customer = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isWalk = customer.GetBool("isWalk");
        if (lastPos != transform.localPosition && !isWalk)
        {
            print("걸어감");
            lastPos = transform.localPosition;
            customer.SetBool("isWalk", true);
        }
        else if (lastPos == transform.localPosition && isWalk)
        {
            print("멈춤");
            customer.SetBool("isWalk", false);
        }
    }
}
