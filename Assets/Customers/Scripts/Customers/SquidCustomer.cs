using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Grid = UI.BurgerMaker.Scripts.Grid;

namespace Customers.Scripts.Customers
{
[Serializable]
public class SquidCustomer : Customer
{
    [SerializeField] private float wipeOutRate = 0.002f;
    public override void UseSpecialAbility(AbilityEffectBundle bundle)
    {
        Debug.Log("squid!");
        bundle.burgerMaker.SetSkill(0);
        bundle.selector.setSkin(0);
        StartCoroutine(blind(bundle.squidBlind));
    }

    private IEnumerator blind(Image image)
    {
        float opacity = 1.0f;
        
        var tempColor = image.color;
        
        for(int i=0; i<1.0 / wipeOutRate; i++)
        {
            tempColor.a = opacity;
            image.color = tempColor;
            opacity -= wipeOutRate;
            yield return new WaitForSeconds(0.01f);
        }
        
        tempColor.a = 0f;
        image.color = tempColor;
    }
}
}
