using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChasing : MonoBehaviour
{
    public enum View
    {
        TPV,
        FPV
    }

    Vector3 TargetPos; // 목표 위치
    Quaternion TargetRot; // 목표 각도
    public bool isTPV;
    public bool isFPV;
    public GameObject burgerUI;
    public float transSpeed = 2.5f;
    GameObject FPVPos;
    GameObject TPVPos;
    // Start is called before the first frame update
    void Start()
    {
        FPVPos = GameObject.Find("FPV").gameObject;
        TPVPos = GameObject.Find("TPV").gameObject;

        // 시작할 땐 3인칭 view로 시작한다.
        isFPV = false;
        isTPV = true;

        // 처음엔 햄버거 UI 비활성화
        burgerUI.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isTPV) TPV();
        if (isFPV) FPV();
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void SetView(View view)
    {
        switch (view)
        {
            case View.FPV:
            {
                isFPV = true;
                isTPV = false;
                break;
            }
            case View.TPV:
            {
                isFPV = false;
                isTPV = true;
                break;
            }
        }
    }
    private void FPV() // 1인칭 시점 (버거 만들 때)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleSampleCharacterControl>().setAnimIdle(0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleSampleCharacterControl>().enabled = false;

        TargetPos = FPVPos.transform.position;
        TargetRot = FPVPos.transform.rotation;
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * transSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, Time.deltaTime * transSpeed);

        for (int i = 0; i < 2; i++)
            GameObject.Find("Player").transform.GetChild(i).gameObject.SetActive(false);
        //GameObject.Find("Player").transform.Find("BurgerLight").gameObject.SetActive(false);
        burgerUI.SetActive(true);
    }

    private void TPV() // 3인칭 시점 (default)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleSampleCharacterControl>().enabled = true;
        TargetPos = TPVPos.transform.position;
        TargetRot = TPVPos.transform.rotation;
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * transSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, Time.deltaTime * transSpeed);
        burgerUI.SetActive(false);
        //GameObject.Find("Player").transform.Find("BurgerLight").gameObject.SetActive(true);
        for (int i = 0; i < 2; i++)
            GameObject.Find("Player").transform.GetChild(i).gameObject.SetActive(true);
    }
}
