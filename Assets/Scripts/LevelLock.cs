using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLock : MonoBehaviour
{
    public GameObject button2, button3, button4, button5;
    void Start()
    {
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.isClear1)
            button2.gameObject.GetComponent<Button>().interactable = true;
        else
            button2.gameObject.GetComponent<Button>().interactable = false;

        if (GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.isClear2)
            button3.gameObject.GetComponent<Button>().interactable = true;
        else
            button3.gameObject.GetComponent<Button>().interactable = false;

        if (GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.isClear3)
            button4.gameObject.GetComponent<Button>().interactable = true;
        else
            button4.gameObject.GetComponent<Button>().interactable = false;

        if (GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.isClear4)
            button5.gameObject.GetComponent<Button>().interactable = true;
        else
            button5.gameObject.GetComponent<Button>().interactable = false;
    }
}