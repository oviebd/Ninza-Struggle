using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour {

    public static string Tag_soundManager = "SoundManager";

    public static string pref_sound = "Sound_pref";

    public static string pref_is_game_first_launced = "is_Game_Started_Pref";
    public static string pref_bestScore = "gameBestScore";

    public static string GameSceneName = "GamePage";
    public static string MainSceneName = "MainPage";
    public static string SettingSceneName = "SettingPage";


    public static bool getIsSoundOn()
    {

        if (PlayerPrefs.HasKey(pref_sound))
        {
            if (PlayerPrefs.GetInt(pref_sound) == 1)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public static void MakeSoundOn()
    {
        PlayerPrefs.SetInt(pref_sound, 1);
    }

    public static void SwapSound()
    {
        bool val_bool = getIsSoundOn();
        int val_int;

        if (val_bool == true)  // Sount is On ... Make it Off
            val_int = 0;
        else   // Sount is Off ... Make it On
            val_int = 1;

        PlayerPrefs.SetInt(pref_sound, val_int);
    }

    public static bool IsGameFirstLaunced()
    {

        if (PlayerPrefs.HasKey(pref_is_game_first_launced))
        {
            int res = PlayerPrefs.GetInt(pref_is_game_first_launced);
            if (res != null)
            {
                if (res == 0)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public static int GetBestScore()
    {
        if (PlayerPrefs.HasKey(pref_bestScore))
        {
            int res = PlayerPrefs.GetInt(pref_bestScore);
            if (res != null)
            {
                return res;
            }
            else
                return 0;

        }
        else
            return 0;
    }

    public static int GetStoredZemNoByType(CollectableSavedData savedData, VariableUtilities.collectableType type) 
    {
        int zemNo;
        switch (type)
        {
            case VariableUtilities.collectableType.BlueCrystal:
                zemNo = savedData.blueCrystalNo;
                break;
            case VariableUtilities.collectableType.GreenCrystal:
                zemNo = savedData.greenCrystalNo;
                break;
            case VariableUtilities.collectableType.PurpleCrystal:
                zemNo = savedData.purpleCrystalNo;
                break;
            case VariableUtilities.collectableType.RedCrystal:
                zemNo = savedData.redCrystalNo;
                break;
            case VariableUtilities.collectableType.TurquoiseCrystal:
                zemNo = savedData.turquoiseCrystalNo;
                break;
            case VariableUtilities.collectableType.YeallowCrystal:
                zemNo = savedData.yeallowCrystalNo;
                break;

            default:
                zemNo = 0;
                break;
        }
        return zemNo;
    }

    public static void IncreaseSpecificZemNo( VariableUtilities.collectableType type, string increseRateString=null)
    {
        CollectableSavedData savedData = DataHandler.instance.LoadCollectableData();
        int increseRate=1;
        if (increseRateString != null)
        {
            increseRate = System.Convert.ToInt32(increseRateString);
          //  Debug.Log("Increase Number : " + increseRate);
        }
        switch (type)
        {
            case VariableUtilities.collectableType.BlueCrystal:
                savedData.blueCrystalNo = savedData.blueCrystalNo+increseRate;
                break;
            case VariableUtilities.collectableType.GreenCrystal:
                 savedData.greenCrystalNo = savedData.greenCrystalNo+ increseRate;
                break;
            case VariableUtilities.collectableType.PurpleCrystal:
                savedData.purpleCrystalNo = savedData.purpleCrystalNo+ increseRate;
                break;
            case VariableUtilities.collectableType.RedCrystal:
                savedData.redCrystalNo= savedData.redCrystalNo+ increseRate;
                break;
            case VariableUtilities.collectableType.TurquoiseCrystal:
                savedData.turquoiseCrystalNo=savedData.turquoiseCrystalNo+ increseRate;
                break;
            case VariableUtilities.collectableType.YeallowCrystal:
                savedData.yeallowCrystalNo=savedData.yeallowCrystalNo+ increseRate;
                break;
           
        }
        DataHandler.instance.SaveCollectableData(savedData);
    }

}
