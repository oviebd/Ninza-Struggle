using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableUtilities : MonoBehaviour {

    public  enum collectableType { BlueCrystal, GreenCrystal, PurpleCrystal, RedCrystal , TurquoiseCrystal,YeallowCrystal };
    public static collectableType type;

    public enum PlayerNamesEnum {Rogue1, Rogue2, Rogue3, Rogue4, Rogue5, Rogue6 };
    public static PlayerNamesEnum playerName;

    public enum GameMode { GameMode_New , GameModeContinue , GameModeInfinity };
    public static GameMode gameMode;

    public enum GameState { Running, Paused, GameOver,Menu };
    public static GameState gameState = GameState.Menu;

    public enum StageName { Stage1, Stage2, Stage3, Stage4, Stage5, Stage6, Stage7, Stage8, Stage9, Stage10 };
    public static StageName stageName;

    public enum SoundType { menuSound, stageSound, bossSound};
    public static SoundType soundType;

    public enum AdsEventType { NormalAds, GemCollectAds, PlayerReviveAds };
    public static AdsEventType adsEventType;

    public static  collectableType zemTypeForAds;
}
