using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public float y = 2.5f;
    private GameObject m_goHpBar;
    void Start()
    {
        m_goHpBar = GameObject.Find("TimerBar/TimerBarImage");
    }

    void Update()
    {
        // 오브젝트에 따른 HP Bar 위치 이동
        //m_goHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, y, 0));
    }
}