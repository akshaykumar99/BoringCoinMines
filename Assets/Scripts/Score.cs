using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
  //  public static Score Instance { set; get; }
    public float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 100;
    private int scoreToNextLevel = 20;
    private bool isDead = false;
    public Text scoreText;
    public DeathMenu deathMenu;
    private const int coin_amount = 5;
    // Start is called before the first frame update
  /*  void Start()
    {
        scoreText.text = "hello";
    }
*/
    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        if (score >= scoreToNextLevel)
            LevelUp();
        score += 0.1f  ;
       // score += Time.deltaTime*difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }
    
    void LevelUp()
    {
        if(difficultyLevel == maxDifficultyLevel)
            return;
        scoreToNextLevel *=3;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);

        Debug.Log(difficultyLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        if(PlayerPrefs.GetFloat("Highscore") < score)
            PlayerPrefs.SetFloat("Highscore",score);
        deathMenu.ToggleEndMenu(score);
    }
   /* public void GetCoin()
    {
        score+=coin_amount;
        scoreText.text = ((int)score).ToString();
    }*/

}
