using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{

    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = this.transform.GetComponent<Button>();
        StartCoroutine("Select", 0.001f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Select()
    {
        if (btn != null)
        {
            btn.onClick.AddListener(GameObject.Find("Select").GetComponent<StageManager>().selectStage);         //스크립트로 버튼 이벤트 수행
        }
    }
}
