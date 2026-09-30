using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
//using Unity.VisualScripting;
//using System;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int hearts;
    [HideInInspector]
    public int extralives;
    [HideInInspector]
    public int currentId;
    [HideInInspector]
    public int random;

    public Sprite[] sprites;

    public GameObject[] canvases;

    int numberOfQuestion;

    [HideInInspector]
    public int valuePoints;

    public GameObject title;
    public GameObject but1;
    public GameObject but2;
    public GameObject but3;
    public GameObject but4;
    public GameObject top;

    [HideInInspector]
    public List<GameObject> availableButtons = new List<GameObject>();
    [HideInInspector]
    public bool chanceToLoseActive;


    [Header("———————  VideoSettings  ———————")]
    public GameObject UIvideo;
    public GameObject phraseBack;
    public Sprite AlbusTexture;
    public Sprite RonTexture;

    public GameObject zamok;
    //saving info
    [HideInInspector]
    bool soundOn = true;
    //[HideInInspector]
    int ochki = 0;

    public GameObject secretPanel;

    public bool SoundOn 
    {
        set 
        {
            if (value == true)
            {
                soundOn = true;
                GameObject.Find("Sound").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                foreach (AudioSource i in Camera.main.GetComponents<AudioSource>()) 
                {
                    i.volume = 1f;
                }
                gameObject.GetComponent<SaveLoadScript>().MySave();
            }
            else if (value == false) 
            {
                soundOn = false;
                GameObject.Find("Sound").GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.6f);
                foreach (AudioSource i in Camera.main.GetComponents<AudioSource>())
                {
                    i.volume = 0f;
                }
                gameObject.GetComponent<SaveLoadScript>().MySave();
            }
        }
        get { return soundOn; }
    }

    
    public int TotalScores
    {
        set
        {
            if (value >= ochki) 
            {
                ochki = value;
                RefreshSecretPanel(value);
            }
                
            
            
        }
        get { return ochki; }
    }


    List<Questions.Question> questions; 
    List<Questions.QuestionImage> questionsImg;

    
    void RefreshSecretPanel(int val) 
    {
        if (val < 100)
            secretPanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0} баллов до <color=yellow>СЕКРЕТА</color> ",100 - val);
        else 
        {
            secretPanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Узнать <color=yellow>СЕКРЕТ</color>");
            zamok.SetActive(false);
            secretPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
            
    }

    void Start()
    {
        //RefreshSecretPanel(15);
        StopAllCoroutines();
        //YandexGame.FullscreenShow();
        questions = Camera.main.GetComponent<Questions>().list;
        questionsImg = Camera.main.GetComponent<Questions>().listImg;
        hearts = 3;
        extralives = 3;
        numberOfQuestion = 0;
        valuePoints = 0;
        //StartCoroutine("ShowAd");

        chanceToLoseActive = false;
    }

    IEnumerator ShowAd()
    {
        yield return new WaitForSeconds(5f);
        YandexGame.FullscreenShow();
        StopCoroutine("ShowAd");
    }


    public void SetQuestion() 
    {
        phraseBack.SetActive(false);
        chanceToLoseActive = false;
        //Debug.Log(numberOfQuestion);
        if (numberOfQuestion % 3 == 0 && numberOfQuestion != 0)
        {
            SetImageQuestion();
            return;
        }
        else 
        {
            numberOfQuestion++;
        }
        random = Random.Range(0, questions.Count);
        title.GetComponentInChildren<TextMeshProUGUI>().text = questions[random].title;
        but1.GetComponentInChildren<TextMeshProUGUI>().text = questions[random].answer1;
        but2.GetComponentInChildren<TextMeshProUGUI>().text = questions[random].answer2;
        but3.GetComponentInChildren<TextMeshProUGUI>().text = questions[random].answer3;
        but4.GetComponentInChildren<TextMeshProUGUI>().text = questions[random].answer4;
        top.GetComponent<Image>().sprite = sprites[0];

        availableButtons.Clear();
        availableButtons.Add(but1);
        availableButtons.Add(but2);
        availableButtons.Add(but3);
        availableButtons.Add(but4);

        currentId = questions[random].id;
        questions.Remove(questions[random]);

        but1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        but2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        but3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        but4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        but1.GetComponent<Button>().interactable = true;
        but2.GetComponent<Button>().interactable = true;
        but3.GetComponent<Button>().interactable = true;
        but4.GetComponent<Button>().interactable = true;




    }

    void SetImageQuestion()
    {
        phraseBack.SetActive(false);
        chanceToLoseActive = false;
        numberOfQuestion++;
        random = Random.Range(0, questionsImg.Count);
        title.GetComponentInChildren<TextMeshProUGUI>().text = questionsImg[random].title;
        but1.GetComponentInChildren<TextMeshProUGUI>().text = questionsImg[random].answer1;
        but2.GetComponentInChildren<TextMeshProUGUI>().text = questionsImg[random].answer2;
        but3.GetComponentInChildren<TextMeshProUGUI>().text = questionsImg[random].answer3;
        but4.GetComponentInChildren<TextMeshProUGUI>().text = questionsImg[random].answer4;
        top.GetComponent<Image>().sprite = sprites[questionsImg[random].imageId];

        availableButtons.Clear();
        availableButtons.Add(but1);
        availableButtons.Add(but2);
        availableButtons.Add(but3);
        availableButtons.Add(but4);

        currentId = questionsImg[random].id;
        questionsImg.Remove(questionsImg[random]);

        but1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        but2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        but3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        but4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        but1.GetComponent<Button>().interactable = true;
        but2.GetComponent<Button>().interactable = true;
        but3.GetComponent<Button>().interactable = true;
        but4.GetComponent<Button>().interactable = true;
    }


    public void FiftyChance() 
    {
        List<GameObject> gameObjs = new List<GameObject>();
        for (int i = 0; i < availableButtons.Count; i++)
        {
            gameObjs.Add(availableButtons[i]);
        }

        //если, например, правильный ответ с id 3 (т.е. 4 кнопка), то рандомизируем остальные кнопки ответов и тд.
        switch (currentId) 
        {
            case 0:
                gameObjs.Remove(but1);
                break;
            case 1:
                gameObjs.Remove(but2);
                break;
            case 2:
                gameObjs.Remove(but3);
                break;
            case 3:
                gameObjs.Remove(but4);
                break;
        }
        if (gameObjs.Count == 3) SetBTN(gameObjs[0], gameObjs[1], gameObjs[2]);
        else if (gameObjs.Count == 2) SetBTN(gameObjs[0], gameObjs[1]);
    }

    private void SetBTN(GameObject but1, GameObject but2, GameObject but3) 
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                but1.GetComponent<Button>().interactable = false;
                but1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
                availableButtons.Remove(but1);
                but2.GetComponent<Button>().interactable = false;
                but2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
                availableButtons.Remove(but2);
                break;
            case 1:
                but1.GetComponent<Button>().interactable = false;
                but1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
                availableButtons.Remove(but1);
                but3.GetComponent<Button>().interactable = false;
                but3.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
                availableButtons.Remove(but3);
                break;
            case 2:
                but2.GetComponent<Button>().interactable = false;
                but2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
                availableButtons.Remove(but2);
                but3.GetComponent<Button>().interactable = false;
                but3.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
                availableButtons.Remove(but3);
                break;
        }
    }
    private void SetBTN(GameObject but1, GameObject but2)
    {
        but1.GetComponent<Button>().interactable = false;
        but1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        availableButtons.Remove(but1);
        but2.GetComponent<Button>().interactable = false;
        but2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        availableButtons.Remove(but2);
    }

    public void AlbusHelps()
    {
        top.GetComponent<Image>().sprite = AlbusTexture;
        string ans = GetAnswer(8);
        phraseBack.SetActive(true);
        phraseBack.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Дамблдор: думаю, это - {0}", ans);
        

    }
    

    public void RonHelps() 
    {
        top.GetComponent<Image>().sprite = RonTexture;
        string ans = GetAnswer(2);
        phraseBack.SetActive(true);
        phraseBack.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Рон: не знаю, наверное - {0}", ans);
        

    }

    




    //в параметре по сути задаем шанс правильного ответа,от0 до 10, чем больше число, тем выше шанс
    string GetAnswer(int level)
    {
        List<GameObject> gameObjs = new List<GameObject>();
        for (int i = 0; i < availableButtons.Count; i++)
        {
            gameObjs.Add(availableButtons[i]);
        }

        if (Random.Range(0, 10) < level)
        {
            switch (currentId)
            {
                case 0:
                    return but1.GetComponentInChildren<TextMeshProUGUI>().text;
                case 1:
                    return but2.GetComponentInChildren<TextMeshProUGUI>().text;
                case 2:
                    return but3.GetComponentInChildren<TextMeshProUGUI>().text;
                case 3:
                    return but4.GetComponentInChildren<TextMeshProUGUI>().text;
                default: return "Не знаю";
            }
        }
        else
        {
            if (gameObjs.Count > 1)
            {
                switch (currentId)
                {
                    case 0:
                        gameObjs.Remove(but1);
                        break;
                    case 1:
                        gameObjs.Remove(but2);
                        break;
                    case 2:
                        gameObjs.Remove(but3);
                        break;
                    case 3:
                        gameObjs.Remove(but4);
                        break;
                }

            }
            return gameObjs[Random.Range(0, gameObjs.Count)].GetComponentInChildren<TextMeshProUGUI>().text;


        }



    }


}
