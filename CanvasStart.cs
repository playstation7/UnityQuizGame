using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStart : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.Find("GameController").GetComponent<GameController>().SetQuestion();
    }

    
}
