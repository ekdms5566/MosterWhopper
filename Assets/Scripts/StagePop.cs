using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePop : MonoBehaviour
{
    public GameObject StageCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopStage()
    {
        StageCanvas.SetActive(true);
    }
}
