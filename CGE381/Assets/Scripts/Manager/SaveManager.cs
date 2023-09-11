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
    public string[] namePlayer;
    public int[] star;
    public string[] nameMap;
    public int numSave;

    private void OnEnable()
    {
        LoadSave(0);
        LoadSave(1);
        LoadSave(2);
    }
    private void OnDisable()
    {
        //ResetSave();
    }
    public void SaveGame(int indexSlotSave)
    {
        if (indexSlotSave == 0)
        {
            PlayerPrefs.SetString("Plyer1", namePlayer[indexSlotSave]);
            PlayerPrefs.SetInt("Star1", star[indexSlotSave]);
            PlayerPrefs.SetString("Map1", nameMap[indexSlotSave]);
        }
        else if (indexSlotSave == 1)
        {
            PlayerPrefs.SetString("Plyer2", namePlayer[indexSlotSave]);
            PlayerPrefs.SetInt("Star2", star[indexSlotSave]);
            PlayerPrefs.SetString("Map2", nameMap[indexSlotSave]);
        }
        else if (indexSlotSave == 2)
        {
            PlayerPrefs.SetString("Plyer3", namePlayer[indexSlotSave]);
            PlayerPrefs.SetInt("Star3", star[indexSlotSave]);
            PlayerPrefs.SetString("Map3", nameMap[indexSlotSave]);
        }


        Debug.Log("Save Completed");
    }
    public void LoadSave(int indexSlotSave)
    {
        if (indexSlotSave == 0)
        {
            namePlayer[indexSlotSave] = PlayerPrefs.GetString("Plyer1", namePlayer[indexSlotSave]);
            star[indexSlotSave] = PlayerPrefs.GetInt("Star1", star[indexSlotSave]);
            nameMap[indexSlotSave] = PlayerPrefs.GetString("Map1", nameMap[indexSlotSave]);
        }
        else if (indexSlotSave == 1)
        {
            namePlayer[indexSlotSave] = PlayerPrefs.GetString("Plyer2", namePlayer[indexSlotSave]);
            star[indexSlotSave] = PlayerPrefs.GetInt("Star2", star[indexSlotSave]);
            nameMap[indexSlotSave] = PlayerPrefs.GetString("Map2", nameMap[indexSlotSave]);
        }
        else if (indexSlotSave == 2)
        {
            namePlayer[indexSlotSave] = PlayerPrefs.GetString("Plyer3", namePlayer[indexSlotSave]);
            star[indexSlotSave] = PlayerPrefs.GetInt("Star3", star[indexSlotSave]);
            nameMap[indexSlotSave] = PlayerPrefs.GetString("Map3", nameMap[indexSlotSave]);
        }
        Debug.Log("LoadSave Completed");
    }

    public void LoadGame(bool newGame, int indexSlotSave)
    {
        if (newGame)
        {
            SceneManager.LoadScene("GamePlay 1");
        }
        else
        {
            SceneManager.LoadScene(nameMap[indexSlotSave]);
        }
    }


}
