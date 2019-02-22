using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneToken : Token
{
    private void OnDestroy()
    {
        //do nothing
    }

    protected new void OnMouseDown()
    {
        Debug.Log("This is a hidden stone");
    }
}
