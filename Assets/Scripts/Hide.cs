using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = this.transform.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (btn != null)
        {
            btn.onClick.AddListener(hideCanvas);         //스크립트로 버튼 이벤트 수행
        }
    }
    public void hideCanvas()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
