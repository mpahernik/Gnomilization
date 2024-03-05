using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dan.Main;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int _scorenumb;
    public UnityEvent<string, int> submitScoreEvent;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TMP_InputField inputname;



    private void Awake()
    {
        _scorenumb = GameManager.Score;
        score.text = "Your score is:  " + _scorenumb.ToString();
    }

    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputname.text, _scorenumb);
    }

}
