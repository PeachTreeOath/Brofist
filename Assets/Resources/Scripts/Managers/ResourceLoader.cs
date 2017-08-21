using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public GameObject defaultBlockFab;
    [HideInInspector]
    public Sprite portraitKidSprite;
    [HideInInspector]
    public Sprite[] portraitGirlHairSprites;

    protected override void Awake()
    {
        base.Awake();
        LoadResources();
    }

    private void LoadResources()
    {
        /*
        portraitGirlHairSprites = LoadSpriteArray("Textures/big-lady-bits/girl-front-dress-{0}", 5);
        defaultBlockFab = Resources.Load<GameObject>("Prefabs/Blocks/DefaultBlock");
        portraitKidSprite = Resources.Load<Sprite>("Textures/tempFaceKid");
        */
    }

}
