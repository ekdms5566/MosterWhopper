using Burger.Scripts;
using UnityEngine;


namespace Burger
{

public class Test : MonoBehaviour
{

    [SerializeField] private BurgerBase burgerBase;

    private float timeElapsed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > 5.0f)
        {
            timeElapsed = 0;
            burgerBase.Clear();
            burgerBase.GenerateRandomFeasible(5);
        }
    }
}
}
