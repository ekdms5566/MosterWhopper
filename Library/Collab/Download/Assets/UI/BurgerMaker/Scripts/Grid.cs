using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BurgerMaker.Scripts
{
public class Grid : MonoBehaviour
{
    [SerializeField]
    private int col;
    [SerializeField] 
    private List<GameObject> gridElementPrefabs;
    [SerializeField]
    private int increment;
    [SerializeField] private Selector _selector;
    private bool enableClickSuffle = false;
    [SerializeField] private Image squidBlind;
    private List<GameObject> _gridElements;
    public class GridInfo
    {
        public int col, increment, size;
        public Grid grid;

        public GridInfo(Grid grid, int col, int increment, int size)
        {
            this.grid = grid;
            this.col = col;
            this.increment = increment;
            this.size = size;
        }
    }
    
    void Start()
    {
        BuildGrid(gridElementPrefabs);

        GridInfo ginfo = new GridInfo(this, col, increment, gridElementPrefabs.Count);
        _selector.SetGridInfo(ginfo);
    }

    public void Shuffle()
    {
        BuildGrid(GridUtil.ShuffleList(gridElementPrefabs,1));
    }
    private void BuildGrid(List<GameObject> elementPrefabs)
    {
        _gridElements = new List<GameObject>();
        
        for (int i = 0; i < elementPrefabs.Count; i++)
        {
            int x = i % col;
            int y = i / col;
            GameObject instantiated = Instantiate
            (
                elementPrefabs[i], 
                transform.position+Vector3.right*increment*x+Vector3.down*increment*y, 
                transform.rotation, transform);
            _gridElements.Add(instantiated);
        }
    }

    public void SetControlInverted(bool val)
    {
        _selector.SetControlInverted(val);
    }
    
    public void DoReset()
    {
        BuildGrid(gridElementPrefabs);
        SetControlInverted(false);
        enableClickSuffle = false;
        _selector.resetPos();
        var tempColor = squidBlind.color;
        tempColor.a = 0f;
        squidBlind.color = tempColor;
    }
    public void  NotifyClick(int index)
    {
        _gridElements[index].GetComponent<GridElement>().OnClicked();
        if(enableClickSuffle) Shuffle();
    }

    public void SetShuffle(bool val)
    {
        enableClickSuffle = val;
    }
}
}
