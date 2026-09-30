using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GetRewardVideoAd : MonoBehaviour
{

    public void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.ErrorVideoEvent += ErrorReward;
    }
    public void OnDisable() => YandexGame.RewardVideoEvent += Rewarded;

    public GameObject heart;
    GameController gmContr;
    private void Start()
    {
        gmContr = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void Rewarded(int id)
    {
        if (id == 0)
        {
            YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "Reward_0" } });
            gmContr.hearts++;
            gmContr.canvases[3].SetActive(false);
            gmContr.canvases[1].SetActive(true);
            gmContr.extralives--;
            heart.SetActive(true);
            heart.GetComponent<Animator>().SetTrigger("AnimPlay");
            Debug.Log("Награда выдана");
        }
        else if (id == 1) 
        {
            YandexMetrica.Send("triggers", new Dictionary<string, string> { { "triggers", "Reward_1" } });
            //метод setScores сохранит увеличенное значение и сразу же отправит его на сервер 
            GameObject.Find("CanvasGameEnd").GetComponent<CanvasGameEnd>().setScores();
            GameObject.Find("GetDoubleAward").GetComponent<Button>().interactable = false;


        }
        foreach (AudioSource audioSource in Camera.main.GetComponents<AudioSource>())
        {
            audioSource.mute = false;
        }

    }
    public void ErrorReward(int id)
    {
        if (id == 0)
        {
            gmContr.canvases[3].SetActive(false);
            gmContr.canvases[2].SetActive(true);
        }
        else if (id == 1)
        {
            GameObject.Find("CanvasGameEnd").GetComponent<CanvasGameEnd>().setScores();
            GameObject.Find("GetDoubleAward").GetComponent<Button>().interactable = false;
        }
        foreach (AudioSource audioSource in Camera.main.GetComponents<AudioSource>())
        {
            audioSource.mute = false;
        }


        Debug.Log("Ошибка выдачи награды!");
    }
}
