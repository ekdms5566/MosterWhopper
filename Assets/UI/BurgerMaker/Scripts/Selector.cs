using System.Collections.Generic;
using UnityEngine;

namespace UI.BurgerMaker.Scripts
{
public class Selector : MonoBehaviour
{
    private int col;
    private int elementN;
    private int increment;
    private Grid _grid;

    private int _index;
    private int _x=0, _y=0;
    private int moveIncrement = 1;

    private AudioSource afx;

    [SerializeField] private List<GameObject> skins;

    bool isWithin(int x, int min, int max)
    {
        return (min <= x && x <= max);
    }

    public void setSkin(int num)
	{
        for (int i = 0; i < skins.Count; i++) skins[i].SetActive(false);

        if (num < 0) return;

        skins[num].SetActive(true);
    }

    
    bool move(int xInc, int yInc)
    {
        if (isWithin(_index + xInc + yInc * col, 0, elementN - 1))
        {
            _index += (xInc+yInc*col);
            //Debug.Log(_index);
            int newX = _index % col;
            int newY = _index / col;
            
            transform.Translate(Vector3.right*(newX-_x)*increment + Vector3.down*(newY-_y)*increment);
            _x = newX;
            _y = newY;
            
            return true;
        }

        return false;
    }

    public void SetGridInfo(Grid.GridInfo ginfo)
    {
        this._grid = ginfo.grid;
        this.elementN = ginfo.size;
        this.col = ginfo.col;
        this.increment = ginfo.increment;
    }
    // Start is called before the first frame update
    void Start()
    {
        afx=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            move(-moveIncrement, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            move(moveIncrement, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            move(0, -moveIncrement);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            move(0, moveIncrement);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _grid.NotifyClick(_index);
            afx.Play();
        }
        
    }

    public void SetControlInverted(bool val)
    {
        if (val) moveIncrement = -1;
        else moveIncrement = 1;
    }

    public void resetPos()
    {
        _index = 0;
        move(0, 0);
    }
}
}
