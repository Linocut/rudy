using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
    public Text enemyCount;
    public int robotCount = 0;
    public int coggieCount = 0; 
    public Text endText;
    public GameObject rudy;
    public bool gameOver = false;
    public bool win = false;
    public bool loseBool = false;
    public static int level = 1;
    public int view;
    public GameObject talk;
    public Text cog;

    void Start()
    {
       endText.gameObject.SetActive(false);
    }
    void Update()
    {
        RubyController rubyCog = rudy.gameObject.GetComponent<RubyController>();
        cog.text = "Cogs: " + rubyCog.currentCog + "\nSpecial Cogs: "+ rubyCog.currentSuperCogs;
        view = level;
        enemyCount.text = "Fixed Robots: " + robotCount.ToString() + "/4\n" + "Cog enemies: " + coggieCount.ToString();
        if ((robotCount == 4 || win == true) && level == 1 )
        {
            talk.gameObject.SetActive(true);

            level = 2;
            win = true;

        }
        if (robotCount == 4 && level == 2 && win == false)
        {
            endText.text = "You win!\nPress 'R' to restart\nGame by Jasmine Darman";
            endText.gameObject.SetActive(true);
            rudy.gameObject.SetActive(false);
            gameOver = true;
            win = true;

        }
        if (Input.GetKey(KeyCode.R))

        {
            if (gameOver == true && level != 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene
            }

            if (gameOver == true && level == 2 && win == true)
            {
                level = 1;
                SceneManager.LoadScene("MainScene"); // this loads the scene 1 again 
            }
            if (gameOver == true && level == 2 && win == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // loads current scene again 
            }
        }
    }

 
    public void change()
    {
        robotCount = robotCount + 1;
    }
    public void CoggieChange()
    {
        coggieCount = coggieCount + 1; 
    }
    public void lose()
    {
        loseBool = true; 

        endText.text = "You lose\nPress 'R' to restart\nGame by Jasmine Darman";
        endText.gameObject.SetActive(true);
        rudy.gameObject.SetActive(false);
        gameOver = true;
    }

}
