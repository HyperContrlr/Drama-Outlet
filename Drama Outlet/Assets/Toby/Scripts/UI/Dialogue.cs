using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
}

