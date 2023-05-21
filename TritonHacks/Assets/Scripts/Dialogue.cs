using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    // Start is called before the first frame update
    public int speaker;
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
    public int[] portraits;
}
