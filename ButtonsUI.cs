using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;
using TMPro;


public class ButtonsUI : MonoBehaviour
{
    GameController gmContr;
    public GameObject rulesPanel;
    public GameObject secretPanel;
   
    private void Start()
    {
        
        gmContr = GameObject.Find("GameController").GetComponent<GameController>();
        if (gameObject.name == "Sound") 
        {
            if (!gmContr.SoundOn)
            {
                gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.6f);
            }
            else 
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

            
        
    }

    public void Pressed(int id)
    {

        GameObject.Find("But1").GetComponent<Button>().interactable = false;
        GameObject.Find("But2").GetComponent<Button>().interactable = false;
        GameObject.Find("But3").GetComponent<Button>().interactable = false;
        GameObject.Find("But4").GetComponent<Button>().interactable = false;
        if (id == gmContr.currentId)
        {
            Camera.main.GetComponents<AudioSource>()[3].Play();
            gameObject.GetComponent<Image>().color = new Color(0f, 255f / 255f, 0f, 1f);
            StartCoroutine(CnangeQuestion());
            GameObject.Find("Slider").GetComponent<SliderScript>().ValueIncreasing();
            
        }
        else
        {
            Camera.main.GetComponents<AudioSource>()[4].Play();
            gameObject.GetComponent<Image>().color = new Color(255f / 255f, 0f, 0f, 1f);
            if (gmContr.chanceToLoseActive) 
            {
                gmContr.chanceToLoseActive = false;
                gmContr.availableButtons.Remove(gameObject);
                for (int i = 0; i < gmContr.availableButtons.Count; i++)
                {
                    gmContr.availableButtons[i].GetComponent<Button>().interactable = true;
                }
                return;
            }
            GameObject.Find("Slider").GetComponent<SliderScript>().ValueDecreasing();
            GameObject.Find(string.Format("Heart{0}", gmContr.hearts--)).gameObject.SetActive(false);
            if (gmContr.hearts == 0)
            {
                gmContr.valuePoints = GameObject.Find("Slider").GetComponent<SliderScript>().value; //передает значение value из слайдера в gamecontroller
                if (gmContr.extralives > 0)
                {
                    gmContr.canvases[3].SetActive(true);
                    gmContr.canvases[1].SetActive(false);
                }
                else 
                {
                    gmContr.canvases[2].SetActive(true);
                    gmContr.canvases[1].SetActive(false);
                }
                
                return;
            }
            StartCoroutine(CnangeQuestion());
        }

        
    }


    IEnumerator CnangeQuestion() 
    {
        yield return new WaitForSeconds(1f);
        gmContr.SetQuestion();
        StopCoroutine(CnangeQuestion());
    }


    public void StartPressed() 
    {
        YandexMetrica.Send("idLevelLoad");
        gmContr.canvases[1].SetActive(true);
        gmContr.canvases[0].SetActive(false);
    }

    public void Restart()
    {
        YandexMetrica.Send("idLevelComplete");
        SceneManager.LoadScene(0);
    }
    public void OpenRulesPanel()
    {
        YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "OpenRules" } });
        Camera.main.GetComponents<AudioSource>()[8].Play();
        rulesPanel.SetActive(true);
    }
    public void CloseRulesPanel()
    {
        Camera.main.GetComponents<AudioSource>()[8].Play();
        rulesPanel.SetActive(false);
    }

    public void FinishTest()
    {
        
        gmContr.canvases[2].SetActive(true);
        gmContr.canvases[3].SetActive(false);
    }
    //50 на 50
    public void HalfCut()
    {
        YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "HalfCut" } });
        gameObject.GetComponent<Button>().interactable = false;
        gmContr.FiftyChance();
        Camera.main.GetComponents<AudioSource>()[5].Play();
    }

    //шанс на ошибку
    public void ChanceToLose()
    {
        YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "ChanceToLose" } });
        Camera.main.GetComponents<AudioSource>()[6].Play();
        gmContr.chanceToLoseActive = true;
        gameObject.GetComponent<Button>().interactable = false;
        
    }
    public void AlbusToHelp()
    {
        YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "AlbusToHelp" } });
        gameObject.GetComponent<Button>().interactable = false;
        gmContr.AlbusHelps();
        Camera.main.GetComponents<AudioSource>()[1].Play();
    }
    public void RonToHelp()
    {
        YandexMetrica.Send("triggers",new Dictionary<string, string> { { "triggers", "RonToHelp" } });
        gameObject.GetComponent<Button>().interactable = false;
        gmContr.RonHelps();
        Camera.main.GetComponents<AudioSource>()[2].Play();
    }

    public void soundTurn() 
    {
        if (gmContr.SoundOn)
        {
            gmContr.SoundOn = false;
        }
        else
        {
            gmContr.SoundOn = true;
        }
    }

  
    public void Lock() 
    {
        
        //GameObject.Find("Lock").GetComponent<Image>().color = new Color (0.7607843f, 0.7607843f, 0.7607843f, 1f);
        if (gmContr.TotalScores >= 100) 
        {
            foreach (AudioSource i in Camera.main.GetComponents<AudioSource>())
            {
                i.mute = true;
            }
            YandexMetrica.Send("idGameComplete");
            
            
            Camera.main.GetComponents<AudioSource>()[0].mute = false;
            Camera.main.GetComponents<AudioSource>()[0].Pause();
            Camera.main.GetComponents<AudioSource>()[10].mute = false;
            Camera.main.GetComponents<AudioSource>()[11].mute = false;
            secretPanel.SetActive(true);

        }else
            Camera.main.GetComponents<AudioSource>()[9].Play();

    }


    

    public void ExitFromSecretPanel() 
    {
        foreach (AudioSource i in Camera.main.GetComponents<AudioSource>())
        {
            i.mute = false;
        }
        
        Camera.main.GetComponents<AudioSource>()[0].Play();
        secretPanel.SetActive(false);
        
    }


    public void DoubleRewardClick() 
    {
        if(gameObject.name != "GetAward")
            gameObject.GetComponent<Button>().interactable = false;
        foreach (AudioSource audioSource in Camera.main.GetComponents<AudioSource>())
        {
            audioSource.mute = true;
        }
    }

    

}
