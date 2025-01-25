namespace Enums
{
    public enum SFX_TYPE
    {
        HIT,
        SHOOT,
        LEVEL_UP,
    }
    
    public enum BGM_TYPE
    {
        PLAY,
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
}