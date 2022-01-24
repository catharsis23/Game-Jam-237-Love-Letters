using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Letter
{
    public string owner;

    [TextArea(3, 10)]
    public string letter;

}
