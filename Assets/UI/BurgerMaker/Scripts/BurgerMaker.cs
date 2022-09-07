using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMaker : MonoBehaviour
{
    [SerializeField] private List<GameObject> skills;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkill(int num)
    {
        for (int i = 0; i < skills.Count; i++) skills[i].SetActive(false);

        if (num < 0) return;

        skills[num].SetActive(true);
    }
}
