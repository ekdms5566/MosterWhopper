using UnityEngine;

namespace UI.BurgerMaker.Scripts
{
public abstract class GridElement : MonoBehaviour
{
    public abstract void OnSelected();
    public abstract void OnClicked();
}
}
