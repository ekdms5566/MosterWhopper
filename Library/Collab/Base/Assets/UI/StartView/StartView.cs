using System.Collections;
using System.Collections.Generic;
using Customers.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    [SerializeField] private List<GameObject> secImages;
    [SerializeField] private CustomerGenerator CustomerGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for(int i=0; i<3; i++) secImages[i].SetActive(false);
        
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("countdown:" + i);
            secImages[i].SetActive(true);
            yield return new WaitForSeconds(1.2f);
            secImages[i].SetActive(false);
        }

        Debug.Log("stop");

        CustomerGenerator.StartStage();
        yield return null;
    }
}
