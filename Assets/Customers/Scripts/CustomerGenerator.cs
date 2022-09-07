using System.Collections.Generic;
using Burger.Scripts;
using UI.ResultView.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Grid = UI.BurgerMaker.Scripts.Grid;
using TMPro;
using UI.BurgerMaker.Scripts;

namespace Customers.Scripts
{
    public class CustomerGenerator : MonoBehaviour
    {
        [SerializeField] private List<CustomerFactory> factories;
        [SerializeField] private List<Transform> waitingLinePoints;
        [SerializeField] private BurgerBase burgerBase;
        [SerializeField] private BurgerBase burgerReference;
        [SerializeField] private BurgerBase burgerPlayer;
        [SerializeField] private BurgerBase burgerBasePlayer;
        [SerializeField] private CameraChasing cam;
        [SerializeField] private GameObject burgerMakerUI;
        [SerializeField] private Grid grid;
        [SerializeField] private Selector selector;
        [SerializeField] private Image squidBlind;
        [SerializeField] private GameObject resultViewUI;
        [SerializeField] private GameObject startUI;
        [SerializeField] private BurgerMaker burgerMaker;
        private int stageNum = 0;
        private float timeElapsed = 10000;
        private List<GameObject> waitingCustomers;
        private GameObject customerServed = null;
        private Customer.AbilityEffectBundle _effectBundle;
        private int score;
        private bool running = false;

        public TextMeshProUGUI txt;
        public AudioSource money;

        int GetStageNum()
        {
            int stageNum = GameObject.Find("SaveCtrl").GetComponent<DataController>().gameDate.SelectedStageNum;
            return stageNum;
        }
        public void OpenStage(int stageN)
        {
            Debug.Log("opening stage " + (GetStageNum()+1) );
            score = 0;
            stageNum = GetStageNum();
            factories[stageNum].DoReset();
            resultViewUI.SetActive(false);
            startUI.SetActive(true);
            timeElapsed = 10000;
            startUI.GetComponent<StartView>().StartCountdown();
        }

        public void StartStage()
        {
            timeElapsed = 10000;
            running = true;
            startUI.SetActive(false);
        }

        public void OpenStage()
        {
            OpenStage(stageNum);
        }

        public void Start()
        {
            _effectBundle = new Customer.AbilityEffectBundle();
            _effectBundle.grid = grid;
            _effectBundle.squidBlind = squidBlind;
            _effectBundle.burgerMaker = burgerMaker;
            _effectBundle.selector = selector;

            waitingCustomers = new List<GameObject>(waitingLinePoints.Count);
            for (int i = 0; i < waitingLinePoints.Count; i++)
            {
                waitingCustomers.Add(null);
            }
            txt=GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>(); 

            money=GetComponent<AudioSource>();

            OpenStage(0);
        }

        public GameObject GetClosestCustomer(Transform tf)
        {
            List<GameObject> validCustomers = new List<GameObject>();
            for (int i = 0; i < waitingCustomers.Count; i++) if (waitingCustomers[i] != null) validCustomers.Add(waitingCustomers[i]);

            if (validCustomers.Count == 0) return null;

            GameObject closestCustomer = validCustomers[0];
            float closestDist = Vector3.Distance(tf.position, validCustomers[0].transform.position);

            for (int i = 1; i < validCustomers.Count; i++)
            {
                float dist = Vector3.Distance(tf.position, validCustomers[i].transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestCustomer = validCustomers[i];
                }
            }

            if (burgerPlayer.GetLevel() != 0) return null; // 서빙하는 경우 return null
            if (closestCustomer.GetComponent<Customer>().GetStatus() != Customer.Status.Waiting) return null;

            if (customerServed != null)
            {
                customerServed.GetComponent<Customer>().SetHighlight(false);
            }

            customerServed = closestCustomer;
            customerServed.GetComponent<Customer>().SetHighlight(true);
            return customerServed;
        }

        public void Update()
        {
            timeElapsed += Time.deltaTime;
            if (running && timeElapsed >= factories[stageNum].GetSpawnRate())
            {
                for (int i = 0; i < waitingCustomers.Count; i++)
                {
                    if (waitingCustomers[i] == null)
                    {
                        if (factories[stageNum].IsWaveOver()) break;
                        GameObject customer = factories[stageNum].Create();
                        customer.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        customer.GetComponent<Customer>().SetWaitingLineWaypoint(this, i, waitingLinePoints[i]);
                        waitingCustomers[i] = customer;
                        break;
                    }
                }
                timeElapsed = 0;
            }

            int j;
            for (j = 0; j < waitingCustomers.Count; j++)
            {
                if (waitingCustomers[j] != null) break;
            }

            if (factories[stageNum].IsWaveOver() && j == waitingCustomers.Count && resultViewUI.activeSelf == false)
            {
                resultViewUI.SetActive(true);
                int starScore = factories[stageNum].getStarScore(score);

                ResultView resultView = resultViewUI.GetComponent<ResultView>();
                Debug.Log("score:" + score + " starScore:" + starScore);
                resultView.setStarScore(starScore);
                resultView.Show();

                if (starScore >= 1)
                {
                    Debug.Log("unlocking level " + (stageNum + 2));
                    DataController data = GameObject.Find("SaveCtrl").GetComponent<DataController>();
                    switch (stageNum + 1)
                    {
                        case 1:
                            {
                                data._gameData.isClear1 = true;
                                if(data._gameData.starNum1<starScore)
                                    data._gameData.starNum1 = starScore;

                                data.SaveGameData();
                                break;
                            }
                        case 2:
                            {
                                data._gameData.isClear2 = true;
                                if (data._gameData.starNum2 < starScore)
                                    data._gameData.starNum2 = starScore;

                                data.SaveGameData();
                                break;
                            }
                        case 3:
                            {
                                data._gameData.isClear3 = true;
                                if (data._gameData.starNum3 < starScore)
                                    data._gameData.starNum3 = starScore;

                                data.SaveGameData();
                                break;
                            }
                        case 4:
                            {
                                data._gameData.isClear4 = true;
                                if (data._gameData.starNum4 < starScore)
                                    data._gameData.starNum4 = starScore;

                                data.SaveGameData();
                                break;
                            }
                        case 5:
                            {
                                if (data._gameData.starNum5 < starScore)
                                    data._gameData.starNum5 = starScore;
                                data.SaveGameData();
                                break;
                            }
                    }
                }
            }
        }

        public void Serve()
        {
            if (burgerPlayer.GetLevel() != 0)
            {
                burgerPlayer.Clear();
                customerServed.GetComponent<Customer>().Serve();
            }
        }

        public GameObject GetCustomerServed()
        {
            return customerServed;
        }

        public void NotifyCustomerGaveUp(Customer customer)
        {
            if (customerServed != null && customerServed.GetComponent<Customer>() == customer)
            {
                customerServed = null;
                burgerReference.Clear();
                burgerBase.Clear();
                burgerPlayer.Clear();
                CloseBurgerMaker();
            }
        }

        public void NotifyCustomerSatisfied()
        {
            score++;
            txt.text=(score*100).ToString();
            resultViewUI.transform.Find("ResultPanel").transform.Find("ScoreUI2").transform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = txt.text;
            money.Play();
            customerServed = null;
        }
        public void NotifyWaitingLineExit(GameObject obj)
        {
            for (int i = 0; i < waitingCustomers.Count; i++)
            {
                if (waitingCustomers[i] == obj)
                {
                    waitingCustomers[i] = null;
                }
            }
        }

        public bool IsOver()
        {
            return factories[stageNum].IsWaveOver();
        }

        public void SetFactoryIdx(int idx)
        {
            stageNum = idx;
            factories[idx].DoReset();
        }

        public void OpenBurgerMaker()
        {
            grid.DoReset();
            burgerMakerUI.SetActive(true);
            cam.SetView(CameraChasing.View.FPV);
            burgerReference.Copy(customerServed.GetComponent<Customer>().GetBurgerBase());
            customerServed.GetComponent<Customer>().UseSpecialAbility(_effectBundle);
        }

        public void CloseBurgerMaker()
        {
            burgerMakerUI.SetActive(false);
            cam.SetView(CameraChasing.View.TPV);
        }
    }
}