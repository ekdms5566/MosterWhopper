using System.Collections;
using System.Collections.Generic;
using Customers.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private bool callFlag = false;
    string name;
    private int stageN;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && !callFlag)
        {
            //GameObject.Find("CustomerGenerator").GetComponent<CustomerGenerator>().OpenStage(stageN);
            
            callFlag = true;
        }
    }

    public void selectStage()
    {
        string stageNum = EventSystem.current.currentSelectedGameObject.name;

        Debug.Log("selectStage:"+stageNum);
        name = "Stage" + stageNum;
        GameObject.Find("InfoCanvas").transform.Find(name).gameObject.SetActive(true);
        GameObject.Find("SaveCtrl").GetComponent<DataController>().gameDate.SelectedStageNum = int.Parse(stageNum)-1;

    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
