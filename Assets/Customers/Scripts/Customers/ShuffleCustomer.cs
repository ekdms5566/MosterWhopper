using System;
using System.Collections;
using UnityEngine;
using Grid = UI.BurgerMaker.Scripts.Grid;

namespace Customers.Scripts.Customers
{
[Serializable]
public class ShuffleCustomer : Customer
{
    [SerializeField] private float shuffleRate = 3.0f;
    public override void UseSpecialAbility(AbilityEffectBundle bundle)
    {
        bundle.burgerMaker.SetSkill(2);
        bundle.selector.setSkin(2);
        StartCoroutine(DoShuffles(bundle.grid));
    }

    IEnumerator DoShuffles(Grid grid)
    {
        float timeElapsed;

        grid.SetShuffle(true);
        yield return null;
    }
}
}
