using System;
using System.Collections;
using System.Timers;
using Burger.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Grid = UI.BurgerMaker.Scripts.Grid;

namespace Customers.Scripts
{
    [Serializable]
    public abstract class Customer : MonoBehaviour
    {
        public class CustomerInfo
        {
            public int burgerLevel;

            public CustomerInfo(int burgerLevel)
            {
                this.burgerLevel = burgerLevel;
            }
        }

        public class AbilityEffectBundle
        {
            public Grid grid;
            public UI.BurgerMaker.Scripts.Selector selector;
            public Image squidBlind;
            public BurgerMaker burgerMaker;
        }

        public enum Status
        {
            Entering,
            Waiting,
            Served,
            Exiting
        }

        [SerializeField] private BurgerBase burgerReference;
        private Transform waitingLineWaypoint;
        private CustomerGenerator generator;
        private CustomerInfo info;
        private NavMeshAgent agent;
        private float waitingTime = 3;
        private float timeElapsed;
        private Text text;
        private bool deleteTrigger = false;
        private Status status = Status.Entering;
        public abstract void UseSpecialAbility(AbilityEffectBundle effectBundle);

        private void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public void SetWaitingLineWaypoint(CustomerGenerator gen, int waypointN, Transform tf)
        {
            generator = gen;
            waitingLineWaypoint = tf;
        }
        void Start()
        {
            StartCoroutine(NavigateTo(waitingLineWaypoint));
        }

        public void setWaitingTime(float time)
        {
            waitingTime = time;
        }

        void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > waitingTime && !deleteTrigger)
            {
                deleteTrigger = true;
                generator.NotifyCustomerGaveUp(this);
                burgerReference.Clear();
                StartCoroutine(NavigateAndDelete(GameObject.Find("WaypointExit").transform));
            }
        }

        IEnumerator NavigateTo(Transform tf)
        {
            while (true)
            {
                agent.destination = tf.position;
                timeElapsed = 0;
                if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)) break;
                yield return null;
            }

            burgerReference.GenerateRandomFeasible(info.burgerLevel);
            status = Status.Waiting;
        }

        IEnumerator NavigateAndDelete(Transform tf)
        {
            SetHighlight(false);
            status = Status.Exiting;
            generator.NotifyWaitingLineExit(gameObject);
            while (true)
            {
                agent.destination = tf.position;
                if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)) break;
                yield return null;
            }
            Destroy(this.gameObject);
            yield return null;
        }
        public void SetCustomerInfo(CustomerInfo info)
        {
            this.info = info;
        }

        public CustomerInfo GetInfo()
        {
            return info;
        }

        public Status GetStatus()
        {
            return status;
        }

        public void ShowReferenceBurger()
        {
            status = Status.Served;
            //burgerReference.GenerateRandomFeasible(info.burgerLevel);
        }

        public BurgerBase GetBurgerBase()
        {
            return burgerReference;
        }

        public void Serve()
        {
            deleteTrigger = true;
            generator.NotifyCustomerSatisfied();
            burgerReference.Clear();
            StartCoroutine(NavigateAndDelete(GameObject.Find("WaypointExit").transform));
        }

        public void SetHighlight(bool val)
        {
            if (val) this.GetComponent<Outline>().OutlineWidth = 10;
            else
            {
                this.GetComponent<Outline>().OutlineWidth = 0;
                status = Status.Waiting;
            }


        }
    }
}