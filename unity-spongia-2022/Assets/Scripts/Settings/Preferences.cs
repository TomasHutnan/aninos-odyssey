using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences
{

    private const string ConfirmSellKey = "pref-confirmsell";
    private const string MuteMusicKey = "pref-mutemusic";
    private const string MuteSFXKey = "pref-mutesfx";

    static Preferences() {
        _confirmSell = Convert.ToBoolean(PlayerPrefs.GetInt(ConfirmSellKey, 1));
        _muteMusic = Convert.ToBoolean(PlayerPrefs.GetInt(MuteMusicKey, 0));
        _muteSFX = Convert.ToBoolean(PlayerPrefs.GetInt(MuteSFXKey, 0));
    }

    private static bool _confirmSell, _muteMusic, _muteSFX;

    public static bool ConfirmSell {
        get { return _confirmSell; }
        set { PlayerPrefs.SetInt(ConfirmSellKey, Convert.ToInt32(value)); _confirmSell = value; }
    }

    public static bool MuteMusic {
        get { return _muteMusic; }
        set { PlayerPrefs.SetInt(MuteMusicKey, Convert.ToInt32(value)); _muteMusic = value; }
    }

    public static bool MuteSFX {
        get { return _muteSFX; }
        set { PlayerPrefs.SetInt(MuteSFXKey, Convert.ToInt32(value)); _muteSFX = value; }
    }

}
