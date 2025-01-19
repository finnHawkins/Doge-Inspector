using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum gameScreen {
    titleMenu,
    gameScreen,
    settingsMenu,
    pauseMenu,
    gameOver,

}

public class gameManager : MonoBehaviour
{

    public GameObject doge;
    public GameObject foxe;
    public GameObject yesBtn;
    public GameObject noBtn;

    public GameObject currentTarget;

    private int playerScore;
    private int wrongAnswers;
    private int hiScore;

    public GameObject menuScreen;
    public GameObject scoreCanvas;
    public GameObject gameOverScreen;
    public GameObject settingsMenu;

    public Light spotlight;
    public static float FLASH_DURATION = 1f;
    public float flashStartTime;


    public Text score;
    public Text wrongCounter;

    public Text goScore;

    public gameScreen gs;

    public bool gamePaused;

    private bool returnToMainMenu;
    private bool restart;

    private Vector3 dogeStartPos;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        gs = gameScreen.titleMenu;
        gamePaused = true;
        restart = false;

        playerScore = 0;
        wrongAnswers = 0;

        dogeStartPos = new Vector3(-5.25f, 2f, 4f);

        scoreCanvas.SetActive(false);
        gameOverScreen.SetActive(false);
        settingsMenu.SetActive(false);

        Screen.fullScreen = true;

        if (PlayerPrefs.HasKey("hiscore")) {
            hiScore = PlayerPrefs.GetInt("hiscore");
        }

    }

    // Update is called once per frame
    void Update() {

        if(Input.GetKeyUp("escape")) {
            resetGame(false); 

        }

        if(Input.GetKeyUp("space")) {
            if(gs == gameScreen.gameOver) {
                resetGame(true);
            }
        }

        if(spotlight.color == Color.red) {
            if(Time.time >= flashStartTime + FLASH_DURATION) {
                spotlight.color = Color.white;
            }
        }

    }

    public void makeNewDog() {

        int rand = Random.Range(1, 3);

        if(currentTarget != null) {
            updateScore(currentTarget.GetComponent<doge>().correctStatus);
        }

        if(wrongAnswers < 3) {

            //GameObject currentDog = currentTarget;

            Destroy(currentTarget);

            //GameObject newdog;

            Quaternion dogRotation = new Quaternion(3, 180, 0, 0);

            if(rand == 1 || (playerScore == 0 && wrongAnswers == 0)) {
                currentTarget = Instantiate(doge, dogeStartPos, dogRotation);
                currentTarget.GetComponent<doge>().isDog = true;
            } else {
                currentTarget = Instantiate(foxe, dogeStartPos, dogRotation);
                currentTarget.GetComponent<doge>().isDog = false;
            }   

            //Destroy(currentDog);

            currentTarget.name = "Dog";

            yesBtn.GetComponent<statusBtn>().changeDoge(currentTarget);
            noBtn.GetComponent<statusBtn>().changeDoge(currentTarget);
        
            currentTarget.GetComponent<doge>().Start();

            //currentTarget = newdog;
		} else {

            Destroy(currentTarget);

        }

    }

    public void quitGame() {

        Application.Quit();
        Debug.Log("exiting game..");

    }

    public void toggleMenu() {

        switch(gs) {
            case gameScreen.titleMenu:
                gs = gameScreen.gameScreen;
                menuScreen.SetActive(false);
                scoreCanvas.SetActive(true);
                score.text = "Score: " + playerScore;
                wrongCounter.text = "Failed Inspections: " + wrongAnswers + "/3";
                makeNewDog();
                gamePaused = false;
                break;    
            case gameScreen.pauseMenu:
                gs = gameScreen.gameScreen;
                menuScreen.SetActive(false);
                gamePaused = false;
                break;            
            case gameScreen.gameScreen:
                gs = gameScreen.pauseMenu;
                menuScreen.SetActive(true);
                gamePaused = true;
                break;
            case gameScreen.settingsMenu:
                break;
            case gameScreen.gameOver:
                if(restart) {
                    gs = gameScreen.gameScreen;
                    score.text = "Score: " + playerScore;
                    wrongCounter.text = "Failed Inspections: " + wrongAnswers + "/3";
                    gamePaused = false;
                    makeNewDog();
                } else {
                    gs = gameScreen.titleMenu;
                    menuScreen.SetActive(true);

                }
                gameOverScreen.SetActive(false);
                break;
            
        }

    }

    public void toggleSettings() {

        Debug.Log("toggling settings..");

        switch(gs) {
            case gameScreen.titleMenu:

                returnToMainMenu = true;
                gs = gameScreen.settingsMenu;
                settingsMenu.SetActive(true);
                break;

            case gameScreen.pauseMenu:

                returnToMainMenu = false;
                gs = gameScreen.settingsMenu;
                settingsMenu.SetActive(true);
                break;

            case gameScreen.settingsMenu:

                gs = returnToMainMenu ? gameScreen.titleMenu : gameScreen.pauseMenu;
                settingsMenu.SetActive(false);
                break;
            
        }
        
    }

    public void updateScore(bool correctAnswer) {

        if(correctAnswer) {

            playerScore += 1;
            
            score.text = "Score: " + playerScore;

        } else {

            wrongAnswers += 1;
            wrongCounter.text = "Failed Inspections: " + wrongAnswers + "/3";

            if(wrongAnswers == 3) {

                hiScore = playerScore > hiScore ? playerScore : hiScore;
                gs = gameScreen.gameOver;
                gameOverScreen.SetActive(true);
                gamePaused = true;
                goScore.text = "Score: " + playerScore + "\n High Score: " + hiScore;

                saveHiScore();

            }

        }

    }

    public void resetGame(bool restart) {

        playerScore = 0;
        wrongAnswers = 0;
        currentTarget = null;
        this.restart = restart;
        gameOverScreen.SetActive(false);
        toggleMenu();

    }

    public void toggleFullScreen() {

        Screen.fullScreen = !Screen.fullScreen;

    }


    public void flashLight() {

        spotlight.color = Color.red;
        flashStartTime = Time.time;

	}

	public void saveHiScore() {

        PlayerPrefs.SetInt("hiscore", hiScore);
        PlayerPrefs.Save();
	}

}
