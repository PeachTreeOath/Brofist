using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private PlayingField topField;
    private PlayingField bottomField;

    private PlayerController p1;
    private PlayerController p2;
    private PlayerController p3;
    private PlayerController p4;

    private Dictionary<PlayerController, PlayerController> partnerMap = new Dictionary<PlayerController, PlayerController>();

    protected override void Awake()
    {
        base.Awake();

        topField = GameObject.Find("PlayingFieldA").GetComponent<PlayingField>();
        bottomField = GameObject.Find("PlayingFieldB").GetComponent<PlayingField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterPlayer(PlayerController playerController)
    {
        if (playerController.id == 1)
        {
            p1 = playerController;
            topField.AssignPlayer(p1);
        }
        if (playerController.id == 2)
        {
            p2 = playerController;
            bottomField.AssignPlayer(p2);
        }
        if (playerController.id == 3)
        {
            p3 = playerController;
            topField.AssignPlayer(p3);
        }
        if (playerController.id == 4)
        {
            p4 = playerController;
            bottomField.AssignPlayer(p4);
        }

        // Populate partner map once all players registered
        if (p1 != null && p2 != null && p3 != null && p4 != null)
        {
            partnerMap[p1] = p2;
            partnerMap[p2] = p1;
            partnerMap[p3] = p4;
            partnerMap[p4] = p3;
        }
    }

    public void Swap(PlayerController playerController)
    {
        PlayerController partner = partnerMap[playerController];

        if (topField.ContainsPlayer(playerController))
        {
            topField.PlaceInField(partnerMap[playerController]);
            bottomField.PlaceInField(playerController);
        }
        else
        {
            topField.PlaceInField(playerController);
            bottomField.PlaceInField(partnerMap[playerController]);
        }
    }
}

