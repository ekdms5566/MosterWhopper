using System;
using System.Collections;
using Customers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI.ResultView.Scripts
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private GameObject[] stars;
        [SerializeField] private Text timeText;
        [SerializeField] private CustomerGenerator customerGenerator;
        [SerializeField] private Button retryBtn;
        [SerializeField] private GameObject timesupUI;
        [SerializeField] private GameObject mainUIPanel;

        private float startTime;
        public AudioSource endafx;
        public GameObject scoreUI;

        public void Start()
        {
            retryBtn.GetComponent<Button>().onClick.AddListener(OnRetryBtnClicked);
            endafx=GetComponent<AudioSource>();
        }

        public void StartTimer()
        {
            startTime = Time.time;
        }

        public void Show()
        {
            Debug.Log("showing result");
            StartCoroutine(ShowAfterTimesUp());
        }

        public IEnumerator ShowAfterTimesUp()
        {
            endafx.Play();
            
            GameObject.Find("BGM").GetComponent<AudioObject>().Stop();
            timesupUI.SetActive(true);
            mainUIPanel.SetActive(false);
            scoreUI.SetActive(false);


            float clearTime = Time.time - startTime;


            yield return new WaitForSeconds(3);
            mainUIPanel.SetActive(true);
            GameObject.Find("MenuSet").transform.Find("opacity").gameObject.SetActive(true);
            timesupUI.SetActive(false);

            timeText.text = ((int)clearTime / 60).ToString("00") + ":" + ((int)clearTime % 60).ToString("00") + ":" + ((int)(clearTime * 100.0f) % 100).ToString("00");

            yield return null;
            GameObject.Find("ResultPanel").GetComponent<AudioSource>().Play();
        }
        private void OnRetryBtnClicked()
        {
            SceneManager.LoadScene("GameScene");
            GameObject.Find("BGM").GetComponent<AudioObject>().Play();
            customerGenerator.OpenStage();
            customerGenerator.StartStage();
        }

        private void OnSelectStageBtnClicked()
        {
            Debug.Log("btn!!");
        }
        public void setStarScore(int starScore)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i + 1 <= starScore) stars[i].SetActive(true);
                else stars[i].SetActive(false);
            }
        }
    }
}