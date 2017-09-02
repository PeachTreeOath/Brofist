using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the queue of input frames in addition to the button press/release states
/// </summary>
public class InputBuffer {

    private int size = 30;

    private bool aAllowed = true;
    private bool bAllowed = true;
    private bool cAllowed = true;
    private bool dAllowed = true;
    private bool swapAllowed = true;

    private Queue<PlayerInputFrame> inputBuffer = new Queue<PlayerInputFrame>();
    private PlayerInputFrame currentInputFrame;

    public void Enqueue(PlayerInputFrame frame)
    {
        if(inputBuffer.Count >= size)
            inputBuffer.Dequeue();

        currentInputFrame = frame;
        inputBuffer.Enqueue(frame);

        if (frame.IsButtonPressed(PlayerInputButton.A_RELEASE))
            aAllowed = true;
    }

    /// <summary>
    /// Checks if button has been released before allowing it to be pressed.
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool IsButtonPressed(PlayerInputButton button)
    {
        if (button == PlayerInputButton.A && aAllowed)
        {
            bool pressed = currentInputFrame.IsButtonPressed(button);
            aAllowed = !pressed;
            return pressed;
        }
        else if (button == PlayerInputButton.B && bAllowed)
            return false;
        else if (button == PlayerInputButton.C && cAllowed)
            return false;
        else if (button == PlayerInputButton.D && dAllowed)
            return false;
        else if (button == PlayerInputButton.SWAP && swapAllowed)
            return false;

        return currentInputFrame.IsButtonPressed(button);
    }
}
