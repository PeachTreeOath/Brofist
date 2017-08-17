using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents one of the 2 areas you can fight on.
/// </summary>
public class PlayingField : MonoBehaviour
{
    /// <summary>
    /// Sets player as child of playing field game object
    /// </summary>
    /// <param name="player"></param>
    public void AssignPlayer(PlayerController player)
    {
        player.transform.SetParent(transform);
    }

    /// <summary>
    /// Places player into field with same relative position
    /// </summary>
    /// <param name="player"></param>
    public void PlaceInField(PlayerController player)
    {
        Vector2 localPos = player.transform.localPosition;
        AssignPlayer(player);
        player.transform.localPosition = localPos;
    }

    public bool ContainsPlayer(PlayerController player)
    {
        if(player.transform.parent == transform)
        {
            return true;
        }
        return false;
    }
}
