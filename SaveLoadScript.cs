using UnityEngine;
using YG;

public class SaveLoadScript : MonoBehaviour
{
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        GetLoad();
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то выполняем Ваш метод для загрузки
            GetLoad();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    // Ваш метод для загрузки, который будет запускаться в старте
    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        
        GameObject.Find("GameController").GetComponent<GameController>().TotalScores = YandexGame.savesData.totalGameScores;
        GameObject.Find("GameController").GetComponent<GameController>().SoundOn = YandexGame.savesData.soundOn;
    }

    // Допустим, это Ваш метод для сохранения
    public void MySave()
    {
        // Записываем данные в плагин
        // Например, мы хотил сохранить количество монет игрока:
        YandexGame.savesData.totalGameScores = GameObject.Find("GameController").GetComponent<GameController>().TotalScores;
        YandexGame.savesData.soundOn = GameObject.Find("GameController").GetComponent<GameController>().SoundOn;
        // Теперь остается сохранить данные
        YandexGame.SaveProgress();
    }
}
