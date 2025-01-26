namespace Enums
{
    public enum SFX_TYPE
    {
        HIT,
        SHOOT,
        FIRE,
        LEVEL_UP,
        EXPLOSION
    }
    
    public enum VFX_TYPE
    {
        HIT,
        EXPLOSION
    }
    
    public enum BGM_TYPE
    {
        TITLE,
        INGAME,
    }
    
    public enum SPAWN_TYPE
    {
        SPOT,
        SPOT_RANDOM,
        CIRCULAR,
        LINEAR_VERTICAL,
        LINEAR_HORIZONTAL,
        LINEAR_RANDOM,
    }

    public enum SPAWN_CENTER_TYPE
    {
        NONE,
        CENTER,
        CENTER_RANDOM,
        CENTER_TOP,
        CENTER_BOTTOM,
        CENTER_LEFT,
        CENTER_RIGHT,
    }
    
    public enum MOVEMENT_TYPE
    {
        TRACKING,
        LINEAR,
    }

    public enum WEAPON_TYPE
    {
        HAMMER,
        SWORD,
        BOW,
        WAND
    }

    public enum WEAPON_UPGRADE_OPTIONS
    {
        DAMAGE,
        RANGE,
        COOLDOWN,
        FIERCE,
    }

    public enum POPUP_TYPE
    {
        POPUP_SETTING,
        POPUP_PAUSE,
        POPUP_DEAD
    }
}