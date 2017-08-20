using System.Collections.Generic;

/// <summary>
/// Consolidates everything a player can do in one frame.
/// </summary>
public class PlayerInputFrame
{

    private List<PlayerInputButton> inputList = new List<PlayerInputButton>();

    public void AddToFrame(PlayerInputButton button)
    {
        inputList.Add(button);
    }

    public bool IsButtonPressed(PlayerInputButton button)
    {
        if(inputList.Contains(button))
        {
            return true;
        }
        return false;
    }

}
