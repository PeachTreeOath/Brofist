using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : MonoBehaviour {

    public int attackLevel; // Determines blockstun/hitstun, 1-6
    public int damage;
    public List<PlayerInputButton> chainableToList;
    public List<AttackProperty> specialProperties; // TODO: Use enum to signify knockdown, launch, etc

    public int startupFrames;
    public int activeFrames;
    public int recoveryFrames;
    public Sprite startupSprite;
    public Sprite activeSprite;
    public Sprite recoverySprite;
}
