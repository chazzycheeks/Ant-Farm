using UnityEngine;

public class EntityStateTriggers : MonoBehaviour
{
    /// <summary>
    /// Used in Roaming state to detect whether a player is in sight of the entity or not, if it is then swap to survey.
    /// </summary>
    public bool noticedPlayer;
    /// <summary>
    /// Used in survey and spotting timer logic, if it is on then do not increase the idle timer and reset it to 0, else increase it
    /// </summary>
    public bool isSpotting;
    /// <summary>
    /// Used to check whether it finished emerging or not
    /// </summary>
    public bool transitioned;
    /// <summary>
    /// Used to check whether the player is in front of the entity or not.
    /// </summary>
    public bool inFrontOfEntity;
}
