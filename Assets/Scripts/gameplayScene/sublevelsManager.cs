using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class sublevelsManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject target4;

    [SerializeField] GameObject windCanvas;
    [SerializeField] TextMeshProUGUI windText;
    [SerializeField] GameObject windIcon;

    int arrowCounter;
    bool ended = false;
    playerController playerController;

    void Start()
    {
        gameManager.gameManagerInstance.sublevel = 1;
        gameManager.gameManagerInstance.SaveProgress();
        playerController = GameObject.Find("Player").GetComponent<playerController>();
    }

    void Update()
    {
        if (playerController != null)
        {
            arrowCounter = playerController.arrowCounter;
            if ((arrowCounter <= 0 || gameManager.gameManagerInstance.sublevel > 3) && !ended)
            {
                saveStarsProgress((gameManager.gameManagerInstance.sublevel - 1));
                //termina la partida
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene("mainScene");

                ended = true;
            }
        }

    }

    public void OnArrowHit()
    {
        gameManager.gameManagerInstance.sublevel++;
        gameManager.gameManagerInstance.SaveProgress();
        manageSublevels(gameManager.gameManagerInstance.level, gameManager.gameManagerInstance.sublevel);
    }

    Vector3 windRotationIcon;

    public void manageSublevels(int level, int sublevel)
    {
        if (level == 1)
        {
            if (sublevel == 1)
            {
                player.transform.position = new Vector3(-24f, 3.33f, 143f);
            }
            else if (sublevel == 2)
            {
                player.transform.position = new Vector3(-24f, 3.33f, 148f);
            }
            else if (sublevel == 3)
            {
                player.transform.position = new Vector3(-24f, 3.33f, 153f);
            }
        }
        else if (level == 2)
        {
            // target movement
            targetsController tController = GameObject.Find("Target2").GetComponent<targetsController>();
            if (tController != null)
            {
                if (sublevel == 1)
                {
                    player.transform.position = new Vector3(55f, 4f, 60f);
                    tController.setSpeed(0.7f);
                }
                else if (sublevel == 2)
                {
                    player.transform.position = new Vector3(48f, 4f, 60f);
                    tController.setSpeed(1f);
                }
                else if (sublevel == 3)
                {
                    player.transform.position = new Vector3(40f, 4f, 60f);
                    tController.setSpeed(1.5f);
                }
            }
            
        } else if (level == 3)
        {
            // wind
            windCanvas.SetActive(true);
            player.transform.position = new Vector3(28f, 4.5f, 150f);
            

            if (sublevel == 1)
            {
                windController.instance.windDirection = new Vector3(1, 0, 0); // wind to the right
                windController.instance.windSpeed = 20f;
                windText.text = "20";
                windRotationIcon = new Vector3(0, 0, 0);
                windIcon.transform.rotation = Quaternion.Euler(windRotationIcon);
            }
            else if (sublevel == 2)
            {
                windController.instance.windDirection = new Vector3(0, -1, 0); // wind down
                windController.instance.windSpeed = 25f;
                windText.text = "25";
                windRotationIcon = new Vector3(0, 0, -90);
                windIcon.transform.rotation = Quaternion.Euler(windRotationIcon);
            }
            else if (sublevel == 3)
            {
                windController.instance.windDirection = new Vector3(0.5f, 0, -1); // wind up and left
                windController.instance.windSpeed = 30f;
                windText.text = "35";
                windRotationIcon = new Vector3(0, 0, 20);
                windIcon.transform.rotation = Quaternion.Euler(windRotationIcon);
            }
        } else if (level == 4)
        {
            // target movement + wind
            windCanvas.SetActive(true);

            targetsController tController = GameObject.Find("Target4").GetComponent<targetsController>();
            if (tController != null)
            {
                if (sublevel == 1)
                {
                    player.transform.position = new Vector3(96f, 5.5f, 12f);
                    tController.setSpeed(0.5f);
                    windController.instance.windDirection = new Vector3(0, 1, 0); // wind up
                    windController.instance.windSpeed = 20f;
                    windText.text = "20";
                    windRotationIcon = new Vector3(0, 0, 90);
                    windIcon.transform.rotation = Quaternion.Euler(windRotationIcon);
                }
                else if (sublevel == 2)
                {
                    player.transform.position = new Vector3(96f, 5.5f, 17f);
                    tController.setSpeed(0.9f);
                    windController.instance.windDirection = new Vector3(0, -1, 1); // wind down and right
                    windController.instance.windSpeed = 30f;
                    windText.text = "35";
                    windRotationIcon = new Vector3(0, 0, -70);
                    windIcon.transform.rotation = Quaternion.Euler(windRotationIcon);
                }
                else if (sublevel == 3)
                {
                    player.transform.position = new Vector3(96f, 5.5f, 22f);
                    tController.setSpeed(1.4f);
                    windController.instance.windDirection = new Vector3(0, 0, -1); // wind left
                    windController.instance.windSpeed = 30f;
                    windText.text = "35";
                    windRotationIcon = new Vector3(0, 0, -180);
                    windIcon.transform.rotation = Quaternion.Euler(windRotationIcon);
                }
            }
            
        }
    }

    void saveStarsProgress(int stars)
    {
        if (gameManager.gameManagerInstance.level == 1)
        {
            gameManager.gameManagerInstance.starsLvl1 = stars;
        } else if (gameManager.gameManagerInstance.level == 2)
        {
            gameManager.gameManagerInstance.starsLvl2 = stars;
        }
        else if (gameManager.gameManagerInstance.level == 3)
        {
            gameManager.gameManagerInstance.starsLvl3 = stars;
        } else if (gameManager.gameManagerInstance.level == 4)
        {
            gameManager.gameManagerInstance.starsLvl4 = stars;
        }
        gameManager.gameManagerInstance.SaveProgress();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("mainScene"))
        {
            mainSceneController mainController = GameObject.Find("level1Btn").GetComponent<mainSceneController>();
            if (mainController != null)
            {
                mainController.checkStars();
            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
