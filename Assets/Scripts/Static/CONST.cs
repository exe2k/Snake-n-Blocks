/// <summary>ALL CONSTANTS STORED HERE! ///</summary>

public class CONST 
{
    //Global:
    public const bool DEV_MODE = true;
    public const int RND_SEED = 2007; //bring my 2007 back to me! 

    //Generator:
    public const int GEN_MIN_WAYPOINTS = 10;   //default 10
    public const int GEN_MAX_WAYPOINTS = 42; 
    public const int GEN_MIN_WP_DISTANCE = 30; // meters
    public const int GEN_MAX_WP_DISTANCE = 90; // meters
    public const int TEXTURE_TILE_FACTOR = 5;  // degrees
    public const int GLOBAL_NORMAL_ANGLE = 0; // degrees


    //Level:
    public const int FOOD_MAX_POINTS = 7;
    public const int OBSTACLE_MAX_POINTS = 15;
    public const int SPAWNER_OBSTACLES_START_DISTANCE = 50;
    public const int SPAWNER_SINGLE_OBJ_CHANCE= 25; //percent NOT USED YET
    public const int SPAWNER_BAD_ONLY_CHANCE= 5; //percent
    public const int SPAWNER_MOVING_CHANCE= 60; //percent


    //Camera
    public const float CAM_OFFSET_X = 4;
    public const float CAM_OFFSET_Y = 4;
    public const float CAM_OFFSET_Z = 4;

    //Player:
    public const float P_BASIC_SPEED = 5;
    public const float P_LINKS_OFFSET = 1.1f;
    public const float P_LINKS_SIDE_SPEED_FACTOR = .75f;



}
