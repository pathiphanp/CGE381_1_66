using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ModeGame
{
    EASY, HARD
}
public class SaveManager : Singletons<SaveManager>
{
    public ModeGame modeGame;
    public string[] _modeGame = new string[3];
    public string[] namePlayer;
    public int[] star;
    public string[] nameMap;
    public int numSave;

    private void OnEnable()
    {
        LoadAll();
    }
    private void OnDisable()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 0)
        {
            ResetSlotSave();
        }
    }
    public void SaveGame(int indexSlotSave)
    {
        if (indexSlotSave == 0)
        {
            PlayerPrefs.SetString("Plyer1", namePlayer[indexSlotSave]);
            PlayerPrefs.SetInt("Star1", star[indexSlotSave]);
            PlayerPrefs.SetString("Map1", nameMap[indexSlotSave]);
            PlayerPrefs.SetString("ModeGame1", modeGame.ToString());
        }
        else if (indexSlotSave == 1)
        {
            PlayerPrefs.SetString("Plyer2", namePlayer[indexSlotSave]);
            PlayerPrefs.SetInt("Star2", star[indexSlotSave]);
            PlayerPrefs.SetString("Map2", nameMap[indexSlotSave]);
            PlayerPrefs.SetString("ModeGame2", modeGame.ToString());
        }
        else if (indexSlotSave == 2)
        {
            PlayerPrefs.SetString("Plyer3", namePlayer[indexSlotSave]);
            PlayerPrefs.SetInt("Star3", star[indexSlotSave]);
            PlayerPrefs.SetString("Map3", nameMap[indexSlotSave]);
            PlayerPrefs.SetString("ModeGame3", modeGame.ToString());
        }
        //Debug.Log(modeGame);
    }
    public void LoadSave(int indexSlotSave)
    {

        if (indexSlotSave == 0)
        {
            namePlayer[indexSlotSave] = PlayerPrefs.GetString("Plyer1", namePlayer[indexSlotSave]);
            star[indexSlotSave] = PlayerPrefs.GetInt("Star1", star[indexSlotSave]);
            nameMap[indexSlotSave] = PlayerPrefs.GetString("Map1", nameMap[indexSlotSave]);
            _modeGame[0] = PlayerPrefs.GetString("ModeGame1", modeGame.ToString());

        }
        else if (indexSlotSave == 1)
        {
            namePlayer[indexSlotSave] = PlayerPrefs.GetString("Plyer2", namePlayer[indexSlotSave]);
            star[indexSlotSave] = PlayerPrefs.GetInt("Star2", star[indexSlotSave]);
            nameMap[indexSlotSave] = PlayerPrefs.GetString("Map2", nameMap[indexSlotSave]);
            _modeGame[1] = PlayerPrefs.GetString("ModeGame2", modeGame.ToString());
        }
        else if (indexSlotSave == 2)
        {
            namePlayer[indexSlotSave] = PlayerPrefs.GetString("Plyer3", namePlayer[indexSlotSave]);
            star[indexSlotSave] = PlayerPrefs.GetInt("Star3", star[indexSlotSave]);
            nameMap[indexSlotSave] = PlayerPrefs.GetString("Map3", nameMap[indexSlotSave]);
            _modeGame[2] = PlayerPrefs.GetString("ModeGame3", modeGame.ToString());
        }
    }

    public void LoadGame(bool newGame, int indexSlotSave)
    {
        if (newGame)
        {
            SceneManager.LoadScene("GamePlay 1");
        }
        else
        {
            if (_modeGame[indexSlotSave] == "EASY")
            {
                modeGame = ModeGame.EASY;
            }
            else
            {
                modeGame = ModeGame.HARD;
            }
            SceneManager.LoadScene(nameMap[indexSlotSave]);
        }
    }
    public void LoadAll()
    {
        LoadSave(0);
        LoadSave(1);
        LoadSave(2);
    }
    void ResetSlotSave()
    {
        PlayerPrefs.SetString("Plyer1", "");
        PlayerPrefs.SetInt("Star1", 0);
        PlayerPrefs.SetString("Map1", "");
        PlayerPrefs.SetString("ModeGame1", "NONE");
        ////
        PlayerPrefs.SetString("Plyer2", "");
        PlayerPrefs.SetInt("Star2", 0);
        PlayerPrefs.SetString("Map2", "");
        PlayerPrefs.SetString("ModeGame2", "NONE");
        ////
        PlayerPrefs.SetString("Plyer3", "");
        PlayerPrefs.SetInt("Star3", 0);
        PlayerPrefs.SetString("Map3", "");
        PlayerPrefs.SetString("ModeGame3", "NONE");
        LoadAll();
    }

}
