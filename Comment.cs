using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Comment : MonoBehaviour
{
    // Start is called before the first frame update
    string[] comments = new string[]
    {
        "Ты молодец!",
        "Так держать!",
        "У тебя хорошо получается!",
        "Отлично!",
        "Продолжай в том же духе!",
        "Очень хорошо!",
    };
    void OnEnable()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = comments[Random.Range(0, comments.Length)];
    }

    
}
