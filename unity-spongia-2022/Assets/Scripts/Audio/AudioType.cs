
namespace AE.Audio
{
    public enum AudioType
    {
        None,

        // Sound Tracks
        //  - Game Location Themes
        ST_LOC_GR_01,
        ST_LOC_RO_01,
        ST_LOC_EG_01,
        ST_SHOP_01,

        //  - Intro Scene
        ST_INTRO_01,

        //  - Fight Location Themes
        ST_FIGHT_GR_NORMAL_01,
        ST_FIGHT_GR_BOSS_01,

        ST_FIGHT_RO_NORMAL_01,
        ST_FIGHT_RO_BOSS_01,

        ST_FIGHT_EG_NORMAL_01,
        ST_FIGHT_EG_BOSS_01,

        // Sound Effects
        //  - UI
        //    - Menu
        SFX_MENU_BUTTON_HOVER_01,
        SFX_MENU_BUTTON_PRESSED_01,

        //  - Fight
        SFX_FIGHT_HIT_NORMAL_01,
        SFX_FIGHT_HIT_CRITICAL_01,
        SFX_FIGHT_HIT_SPELL_01,

        SFX_FIGHT_DODGE_01,
    }
}