using Burger.Scripts;
using Customers.Scripts;
using UnityEngine;

namespace UI.BurgerMaker.Scripts
{
public class EnterGridElement : GridElement
{
    private BurgerBase _burgerBase;
    private BurgerBase _burgerReference;
    private BurgerBase _burgerPlayer;
    private CustomerGenerator generator;


    public void Start()
    {
        _burgerBase = GameObject.Find("BurgerBase").GetComponent<BurgerBase>();
        _burgerReference = GameObject.Find("BurgerReference").GetComponent<BurgerBase>();
        _burgerPlayer = GameObject.Find("BurgerBasePlayer").GetComponent<BurgerBase>();
        generator = GameObject.Find("CustomerGenerator").GetComponent<CustomerGenerator>();
    }
    
    public override void OnSelected()
    {
    }

    public override void OnClicked()
    {
        if (_burgerBase.Compare(_burgerReference))
        {
            _burgerPlayer.Copy(_burgerReference);
            _burgerBase.Clear();
            _burgerReference.Clear();
            _burgerPlayer.GetComponent<BurgerSound>().soundEffect.clip = _burgerPlayer.GetComponent<BurgerSound>().right;
            _burgerPlayer.GetComponent<BurgerSound>().Play();
            generator.CloseBurgerMaker();
            //_stateManager.SetState(StateManager.State.BurgerMakerQuit);
        }
        else
        {
            _burgerPlayer.GetComponent<BurgerSound>().soundEffect.clip = _burgerPlayer.GetComponent<BurgerSound>().wrong;
            _burgerPlayer.GetComponent<BurgerSound>().Play();
            _burgerBase.Clear();
        }
    }
}
}
