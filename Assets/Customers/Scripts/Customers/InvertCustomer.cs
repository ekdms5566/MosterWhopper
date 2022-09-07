using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Grid = UI.BurgerMaker.Scripts.Grid;

namespace Customers.Scripts.Customers
{
[Serializable]
public class InvertCustomer : Customer
{
    public override void UseSpecialAbility(AbilityEffectBundle bundle)
    {
            Debug.Log("invet!");
            bundle.burgerMaker.SetSkill(1);
            bundle.selector.setSkin(1);
            bundle.grid.SetControlInverted(true);
    }
}
}
