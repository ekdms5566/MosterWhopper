using Burger.Scripts;
using UnityEngine;

namespace UI.BurgerMaker.Scripts
{
public class ClearGridElement : GridElement
{
    private BurgerBase _burgerBase;
    
    public void Start()
    {
        _burgerBase = GameObject.Find("BurgerBase").GetComponent<BurgerBase>();
    }
    
    public override void OnSelected()
    {
    }

    public override void OnClicked()
    {
        _burgerBase.Clear();
    }
}
}
