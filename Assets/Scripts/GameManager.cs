using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int scoredPoints = 0;
    [SerializeField] int pointsGoal = 10;
    [SerializeField] int playerLives = 5;
    [SerializeField] int currentPlayerLives;
    public TextMeshProUGUI chestText;
    public TextMeshProUGUI timerText;
    public GameObject livesGroup;
    public GameObject heartIcon;
    public Image damageRepersentation;
    public int minutes;
    public int seconds;
    private float totalTime;
    void Start()
    {
        damageRepersentation.color = new Color32(255,255,255,0);
        currentPlayerLives = playerLives;
        chestText.text = "Chests: "+scoredPoints.ToString()+"/"+pointsGoal.ToString();
        for(int i = 0;i < playerLives;i++) Instantiate(heartIcon,livesGroup.GetComponent<RectTransform>().position,Quaternion.identity,livesGroup.GetComponent<RectTransform>());
        totalTime = minutes*60 + seconds+1;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime-=Time.deltaTime;
        minutes = (int)(totalTime/60);
        seconds = (int)totalTime-(minutes*60);
        timerText.text = minutes.ToString()+":"+(seconds < 10 ? "0"+seconds.ToString():seconds.ToString());

        if(currentPlayerLives <= 0 || (minutes == 0 && seconds == 0)) SceneManager.LoadScene("GameOver");
        if(scoredPoints == pointsGoal) SceneManager.LoadScene("YouWin");
    }

    public void ScorePoint(){
        scoredPoints++;
        chestText.text = "Chests: "+scoredPoints.ToString()+"/"+pointsGoal.ToString();
    }

    public void DamagePlayer()
    {
        currentPlayerLives--;
        int alpha = 255 - (255*currentPlayerLives/playerLives);
        damageRepersentation.color = new Color32(255,255,255,(byte)alpha);
        if(livesGroup.transform.childCount > 0){
            Destroy(livesGroup.transform.GetChild(0).gameObject);
        }
    }
}
