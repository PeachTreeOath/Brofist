using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingAState : PlayerState
{

    private AttackState attackState;
    private int frame;
    private int startupEndFrame;
    private int activeEndFrame;
    private int recoveryEndFrame;
    private Sprite origSprite; //TODO: Demoware to revert sprite

    public override bool ExecuteInput()
    {
        return false;
    }

    public override void ExecuteFrame()
    {
        Debug.Log(frame);
        frame++;
        switch (attackState)
        {
            case AttackState.STARTUP:
                if(frame > startupEndFrame)
                {
                    attackState = AttackState.ACTIVE;
                    player.ChangeSprite(player.attack5A.activeSprite);
                }
                break;
            case AttackState.ACTIVE:
                if (frame > activeEndFrame)
                {
                    attackState = AttackState.RECOVERY;
                    player.ChangeSprite(player.attack5A.recoverySprite);
                }
                break;
            case AttackState.RECOVERY:
                if (frame > recoveryEndFrame)
                {
                    machine.ChangeState<StandingState>();
                    player.ChangeSprite(origSprite); //TODO: demoware to revert sprite
                }
                break;
        }

    }

    public override void EnterState()
    {
        attackState = AttackState.STARTUP;
        frame = 0;
        startupEndFrame = player.attack5A.startupFrames;
        activeEndFrame = player.attack5A.activeFrames + startupEndFrame;
        recoveryEndFrame = player.attack5A.recoveryFrames + activeEndFrame;
        origSprite = player.GetComponent<SpriteRenderer>().sprite; //TODO: demoware to revert sprite
        player.ChangeSprite(player.attack5A.startupSprite);
    }

    public override void ExitState()
    {
        Debug.Log("Exit" + this.GetType() + " State");
    }

}
