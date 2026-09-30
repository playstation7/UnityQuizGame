using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [HideInInspector]
    public int value;
    public float speedIncreasing;
    private float side;
    public GameObject comment;
    GameController gmContr;
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        side = 1f;
        gmContr = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Slider>().value != value)
        {
            gameObject.GetComponent<Slider>().value += side * speedIncreasing * Time.deltaTime;
            if (Mathf.Abs(gameObject.GetComponent<Slider>().value - value) <= 0.05f) { gameObject.GetComponent<Slider>().value = value; }
        }
    }

    public void ValueIncreasing() 
    { 
        if (Random.Range(0, 2) == 1) 
        {
            comment.SetActive(true);
            StartCoroutine("closeComment");
        } 
        value = value < 15 ? ++value : 15; side = 1f;
        if (value == 15) 
        {
            gmContr.valuePoints = GameObject.Find("Slider").GetComponent<SliderScript>().value; //передает значение value из слайдера в gamecontroller
            gmContr.canvases[2].SetActive(true);
            gmContr.canvases[1].SetActive(false);
        }
    }
    public void ValueDecreasing() { value = value > 0 ? --value : 0; side = -1f; }


    IEnumerator closeComment() 
    {
        yield return new WaitForSeconds(3f);
        comment.SetActive(false);
        StopCoroutine("closeComment");
    }
}
