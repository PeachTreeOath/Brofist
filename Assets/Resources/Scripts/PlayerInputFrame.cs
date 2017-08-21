using System.Collections.Generic;

/// <summary>
/// Consolidates everything a player can do in one frame.
/// </summary>
public class PlayerInputFrame
{

    private List<PlayerInputButton> inputList = new List<PlayerInputButton>();
    private bool isFacingRight;

    public PlayerInputFrame(bool isFacingRight)
    {
        this.isFacingRight = isFacingRight;
    }

    public void AddToFrame(PlayerInputButton button)
    {
        inputList.Add(button);
    }

    /// <summary>
    /// Checks if button is one of the buttons pressed this frame.
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool IsButtonPressed(PlayerInputButton button)
    {
        if (inputList.Contains(button))
            return true;

        return false;
    }

    /// <summary>
    /// Checks if this is the ONLY button pressed this frame.
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool IsOnlyButtonPressed(PlayerInputButton button)
    {
        if (inputList.Count == 1 && IsButtonPressed(button))
            return true;

        return false;
    }

    public bool IsForwardPressed()
    {
        if (isFacingRight && IsButtonPressed(PlayerInputButton.RIGHT))
            return true;
        if (!isFacingRight && IsButtonPressed(PlayerInputButton.LEFT))
            return true;
        return false;
    }

    public bool IsBackwardPressed()
    {
        if (isFacingRight && IsButtonPressed(PlayerInputButton.LEFT))
            return true;
        if (!isFacingRight && IsButtonPressed(PlayerInputButton.RIGHT))
            return true;
        return false;
    }

    public bool IsOnlyForwardPressed()
    {
        if (isFacingRight && IsOnlyButtonPressed(PlayerInputButton.RIGHT))
            return true;
        if (!isFacingRight && IsOnlyButtonPressed(PlayerInputButton.LEFT))
            return true;
        return false;
    }

    public bool IsOnlyBackwardPressed()
    {
        if (isFacingRight && IsOnlyButtonPressed(PlayerInputButton.LEFT))
            return true;
        if (!isFacingRight && IsOnlyButtonPressed(PlayerInputButton.RIGHT))
            return true;
        return false;
    }


}
