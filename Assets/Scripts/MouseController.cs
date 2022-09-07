using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Texture2D cursorTextureA;
    public Texture2D cursorTextureB;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        hotSpot.x = cursorTextureA.width / 2;
        hotSpot.y = cursorTextureA.height / 2;
    }
    public void ChangeMouseAMode()
    {
        Cursor.SetCursor(cursorTextureA, hotSpot, cursorMode);
    }
    public void ChangeMouseBMode()
    {
        Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
    }
}