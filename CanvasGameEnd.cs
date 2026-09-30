using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using System;

public class CanvasGameEnd : MonoBehaviour
{
    public GameObject text;
    int value;
    string character;
    public GameObject avatar;

    public GameObject scorePanel;

    public Sprite[] sprites;
    // Start is called before the first frame update


    [HideInInspector]
    GameController gmCntr;

    private void OnEnable() => YandexGame.ReviewSentEvent += GetReview;
    private void OnDisable() => YandexGame.ReviewSentEvent -= GetReview;

    private void GetReview(bool obj)
    {
        if(obj == true)
            YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "WriteReview" } });
        else if(obj == false)
            YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "CloseReview" } });
    }

    private void Awake()
    {
        gmCntr = GameObject.Find("GameController").GetComponent<GameController>();
    }



    void Start()
    {
        Camera.main.GetComponents<AudioSource>()[7].Play();
        value = gmCntr.valuePoints;
        switch (value) 
        {
            case < 3:
                character = "Букля";
                avatar.GetComponent<Image>().sprite = sprites[0];
                break;
            case < 6:
                character = "Рон Уизли";
                avatar.GetComponent<Image>().sprite = sprites[1];
                break;
            case < 10:
                avatar.GetComponent<Image>().sprite = sprites[2];
                character = "Распределяющая шляпа";
                break;
            case < 13:
                avatar.GetComponent<Image>().sprite = sprites[3];
                character = "Гермиона Грейнджер";
                break;
            case <= 15:
                avatar.GetComponent<Image>().sprite = sprites[4];
                character = "Альбус Дамблдор";
                break;
            
        }
            

        text.GetComponent<TextMeshProUGUI>().text = string.Format("Поздравляю, ты набрал {0} баллов. По результатам теста твой персонаж из вселенной Гарри Поттера это - {1}.",value,character);
        if (value == 0) 
        {
            GameObject.Find("GetDoubleAward").GetComponent<Button>().interactable = false;
        }
        if (YandexGame.EnvironmentData.reviewCanShow)
                StartCoroutine("ReviewShow");
        setScores();
        refreshScorePanel();


    }

    IEnumerator ReviewShow() 
    {
        yield return new WaitForSeconds(0.5f);
        YandexGame.ReviewShow(true);
        StopCoroutine("ReviewShow");
    }



    public void setScores() 
    {
        
        gmCntr.TotalScores +=  value;
        if (YandexGame.initializedLB) 
        {
            YandexGame.NewLeaderboardScores("RecordTable", gmCntr.TotalScores);
        }
        refreshScorePanel();

        GameObject.Find("GameController").GetComponent<SaveLoadScript>().MySave();
    }


    void refreshScorePanel() 
    {
        if (100 - gmCntr.TotalScores > 0)
            scorePanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("<color=yellow>СЕКРЕТ</color> откроется через: {0} баллов", 100 - gmCntr.TotalScores);
        else
            scorePanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("<color=yellow>СЕКРЕТ</color> открыт");
    }


}
