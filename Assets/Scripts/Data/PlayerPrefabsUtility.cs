using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabsUtility {

    public static string pref_IsFirstTimeGameOpened = "IsItFirstTime";


    public static bool GetIsItFirstTime()
    {
        if (PlayerPrefs.HasKey(pref_IsFirstTimeGameOpened))
        {
            if (PlayerPrefs.GetInt(pref_IsFirstTimeGameOpened) == 1)
                return true;
            else
                return false;
        }
        else
            return true;
    }
    public static void ChangeIsItFirstTimeFalse()
    {
        PlayerPrefs.SetInt(pref_IsFirstTimeGameOpened, 0);
    }
}
