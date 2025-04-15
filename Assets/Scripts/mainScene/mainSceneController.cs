using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainSceneController : MonoBehaviour
{
    public static Vector3 position;
    public static Vector3 rotation;

    [SerializeField] Button level1;
    [SerializeField] Button level2;
    [SerializeField] Button level3;
    [SerializeField] Button level4;

    [SerializeField] Sprite stars0;
    [SerializeField] Sprite stars1;
    [SerializeField] Sprite stars2;
    [SerializeField] Sprite stars3;

    [SerializeField] Image starsLevel1;
    [SerializeField] Image starsLevel2;
    [SerializeField] Image starsLevel3;
    [SerializeField] Image starsLevel4;

    void Start()
    {
        // gameReset();
        Cursor.lockState = CursorLockMode.None;
        gameManager.gameManagerInstance.LoadProgress();
        checkLevel(gameManager.gameManagerInstance.avaiableLevels);
        updateStars();
    }

    void gameReset()
    {
        gameManager.gameManagerInstance.avaiableLevels = 1;
        gameManager.gameManagerInstance.starsLvl1 = 0;
        gameManager.gameManagerInstance.starsLvl2 = 0;
        gameManager.gameManagerInstance.starsLvl3 = 0;
        gameManager.gameManagerInstance.starsLvl4 = 0;
        gameManager.gameManagerInstance.SaveProgress();
    }

    void checkLevel(int avaiableLevels)
    {
        Button[] levels = { level1, level2, level3, level4 };

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].interactable = (i + 1 <= avaiableLevels);
        }
    }

    public void level1Pressed()
    {
        position = new Vector3(-24f, 3.33f, 143f);
        rotation = new Vector3(0f, -180f, 0f);
        gameManager.gameManagerInstance.level = 1;
        gameManager.gameManagerInstance.SaveProgress();
        openGameplayScene();
    }

    public void level2Pressed()
    {
        position = new Vector3(55f, 4f, 60f);
        rotation = new Vector3(0f, 90f, 0f);
        gameManager.gameManagerInstance.level = 2;
        gameManager.gameManagerInstance.SaveProgress();
        openGameplayScene();
    }

    public void level3Pressed()
    {
        position = new Vector3(28f, 4.5f, 150f);
        rotation = new Vector3(0f, 90f, 0f);
        gameManager.gameManagerInstance.level = 3;
        gameManager.gameManagerInstance.SaveProgress();
        openGameplayScene();
    }

    public void level4Pressed()
    {
        position = new Vector3(96f, 5.5f, 12f);
        rotation = new Vector3(0f, 180f, 0f);
        gameManager.gameManagerInstance.level = 4;
        gameManager.gameManagerInstance.SaveProgress();
        openGameplayScene();
    }

    void openGameplayScene()
    {
        gameManager.gameManagerInstance.sublevel = 1;
        gameManager.gameManagerInstance.SaveProgress();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("gameplayScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("gameplayScene"))
        {
            gameManager.gameManagerInstance.sublevel = 1;
            gameManager.gameManagerInstance.SaveProgress();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void checkStars()
    {
        updateStars();

        gameManager.gameManagerInstance.avaiableLevels++;
        gameManager.gameManagerInstance.sublevel = 1;
        gameManager.gameManagerInstance.SaveProgress();
        checkLevel(gameManager.gameManagerInstance.avaiableLevels);
    }

    void updateStars()
    {
        Sprite[] stars = { stars0, stars1, stars2, stars3 };

        starsLevel1.sprite = stars[gameManager.gameManagerInstance.starsLvl1];
        starsLevel2.sprite = stars[gameManager.gameManagerInstance.starsLvl2];
        starsLevel3.sprite = stars[gameManager.gameManagerInstance.starsLvl3];
        starsLevel4.sprite = stars[gameManager.gameManagerInstance.starsLvl4];
    }
}
