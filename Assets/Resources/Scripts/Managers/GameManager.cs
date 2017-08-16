using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private PlayingField topField;
    private PlayingField bottomField;

    private PlayerController p1;
    private PlayerController p2;
    private PlayerController p3;
    private PlayerController p4;

    protected override void Awake () {
        base.Awake();

        topField = gameObject.AddComponent<PlayingField>();
        bottomField = gameObject.AddComponent<PlayingField>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RegisterPlayer(PlayerController playerController)
    {
        if (playerController.id == 1)
            p1 = playerController;
        if (playerController.id == 2)
            p2 = playerController;
        if (playerController.id == 3)
            p3 = playerController;
        if (playerController.id == 4)
            p4 = playerController;
    }

    public void Swap(PlayerController playerController)
    {

    }
}
