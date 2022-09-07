using Burger.Scripts;
using UnityEngine;

namespace UI.BurgerMaker.Scripts
{
public class BurgerElement : GridElement
{
    [SerializeField]
    private string componentName;
    private BurgerBase burgerBase;

    public void Start()
    {
        burgerBase = GameObject.Find("BurgerBase").GetComponent<BurgerBase>();
    }

    public override void OnSelected()
    {
    }

    public override void OnClicked()
    {
        burgerBase.Add(componentName);
    }
}
}
