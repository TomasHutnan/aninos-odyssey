
namespace AE.Audio
{
    public enum AudioType
    {
        None,

        // Sound Tracks
        //  - Game Location Themes
        OST_LOC_GR_01,
        OST_LOC_RO_01,
        OST_LOC_EG_01,
        OST_SHOP_01,

        //  - Intro Scene
        OST_INTRO_01,

        //  - Fight Location Themes
        OST_FIGHT_GR_NORMAL_INTRO,
        OST_FIGHT_GR_NORMAL_MAIN,
        OST_FIGHT_GR_BOSS_INTRO,
        OST_FIGHT_GR_BOSS_MAIN,

        OST_FIGHT_RO_NORMAL_INTRO,
        OST_FIGHT_RO_NORMAL_MAIN,
        OST_FIGHT_RO_BOSS_INTRO,
        OST_FIGHT_RO_BOSS_MAIN,

        OST_FIGHT_EG_NORMAL_INTRO,
        OST_FIGHT_EG_NORMAL_MAIN,
        OST_FIGHT_EG_BOSS_INTRO,
        OST_FIGHT_EG_BOSS_MAIN,

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