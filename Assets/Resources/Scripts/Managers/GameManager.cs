using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private PlayingField topField;
    private PlayingField bottomField;

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
        throw new NotImplementedException();
    }

    public void Swap(PlayerController playerController)
    {

    }
}
