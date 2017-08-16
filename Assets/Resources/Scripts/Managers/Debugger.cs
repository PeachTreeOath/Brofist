using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : Singleton<Debugger> {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X))
        {
            foreach(PlayerController player in Object.FindObjectsOfType<PlayerController>())
            {
                player.TakeDamage(100);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (PlayerController player in Object.FindObjectsOfType<PlayerController>())
            {
                player.UseMeter(10);
            }
        }
    }
}
