using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadCanvas : MonoBehaviour
{
    bool callFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "StartScene" && callFlag)
        {
            GameObject.Find("StartCanvas").gameObject.SetActive(false);
            GameObject.Find("Asset").gameObject.SetActive(false);
            GameObject.Find("StageCanvasMother").transform.Find("StageCanvas").gameObject.SetActive(true);
            callFlag = false;
        }
    }

    public void reloadCanvas()
    {
        callFlag = true;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
