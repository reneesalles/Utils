using UnityEngine;
using System.Collections;

public class EnumUtils {
    public static PlayerPrefsKeys NEXTLEVEL = PlayerPrefsKeys.NEXTLEVEL;
    public static PlayerPrefsKeys LOADLEVELMODE = PlayerPrefsKeys.LOADLEVELMODE;

    public enum PlayerPrefsKeys {
        NEXTLEVEL,
        LOADLEVELMODE
    }

    #region GameFlow
    public enum EScreenState {
        SPLASH_SCREEN,
        LOADING_SCREEN,
        CREDITS_SCREEN,
        MAIN_MENU_SCREEN,
        IN_GAME_SCREEN
    }

    public enum ESplashState {
        PLAYING,
        DONE
    }

    public enum ELoadingState {
        LOADING,
        WAITING_INPUT,
        DONE
    }

    public enum ECreditsState {
        PLAYING,
        PAUSED,
        DONE
    }

    public enum EMainMenuState {
        MAIN,
        PLAY_DIALOG,
        SETTINGS_DIALOG,
        EXIT_DIALOG
    }

    public enum EGameState {
        PLAYING,
        PAUSED,
        CUTSCENE,
        TUTORIAL
    }
    #endregion
}