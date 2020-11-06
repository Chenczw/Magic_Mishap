using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    // Private Variables
    Text win;

    // Start is called before the first frame update
    void Start()
    {
        win.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreScript.scoreValue == 30)
        {
            win.text = "You Win!";
        }
    }
}