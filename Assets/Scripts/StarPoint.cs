using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPoint : MonoBehaviour
{
    DataController data;
    GameObject stage1, stage2, stage3, stage4, stage5;
    Image star;
    public Sprite activedStar;
    // Start is called before the first frame update
    void Start()
    {
        stage1 = GameObject.Find("1");
        stage2 = GameObject.Find("2");
        stage3 = GameObject.Find("3");
        stage4 = GameObject.Find("4");
        stage5 = GameObject.Find("5");
        data = GameObject.Find("SaveCtrl").GetComponent<DataController>();
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeImage()
    {
        for(int i=0; i< data._gameData.starNum1; i++)
        {
            star = stage1.transform.GetChild(i).GetComponent<Image>();
            star.sprite = activedStar;
        }

        for (int i = 0; i < data._gameData.starNum2; i++)
        {
            star = stage2.transform.GetChild(i).GetComponent<Image>();
            star.sprite = activedStar;
        }

        for (int i = 0; i < data._gameData.starNum3; i++)
        {
            star = stage3.transform.GetChild(i).GetComponent<Image>();
            star.sprite = activedStar;
        }

        for (int i = 0; i < data._gameData.starNum4; i++)
        {
            star = stage4.transform.GetChild(i).GetComponent<Image>();
            star.sprite = activedStar;
        }

        for (int i = 0; i < data._gameData.starNum5; i++)
        {
            star = stage5.transform.GetChild(i).GetComponent<Image>();
            star.sprite = activedStar;
        }
    }
}
