using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager gameManagerInstance { get; private set; }

    public int avaiableLevels;
    public int level;
    public int sublevel;

    public int starsLvl1;
    public int starsLvl2;
    public int starsLvl3;
    public int starsLvl4;

    private void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadProgress();
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("avaiableLevels", avaiableLevels);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("sublevel", sublevel);
        PlayerPrefs.SetInt("starsLvl1", starsLvl1);
        PlayerPrefs.SetInt("starsLvl2", starsLvl2);
        PlayerPrefs.SetInt("starsLvl3", starsLvl3);
        PlayerPrefs.SetInt("starsLvl4", starsLvl4);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        avaiableLevels = PlayerPrefs.GetInt("avaiableLevels", 1);
        level = PlayerPrefs.GetInt("level", 1);
        sublevel = PlayerPrefs.GetInt("sublevel", 1);
        starsLvl1 = PlayerPrefs.GetInt("starsLvl1", 0);
        starsLvl2 = PlayerPrefs.GetInt("starsLvl2", 0);
        starsLvl3 = PlayerPrefs.GetInt("starsLvl3", 0);
        starsLvl4 = PlayerPrefs.GetInt("starsLvl4", 0);
    }
}
