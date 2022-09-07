using UnityEngine;

namespace Customers.Scripts.Customers
{
public class RegularCustomer : Customer
{
    public override void UseSpecialAbility(AbilityEffectBundle bundle)
    {
            bundle.burgerMaker.SetSkill(-1);
            bundle.selector.setSkin(-1);
        }
}
}
