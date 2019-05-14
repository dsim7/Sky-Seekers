﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAPPanel : MonoBehaviour
{
    [SerializeField]
    CharacterPanel characterPanel;
    [SerializeField]
    Image[] apOrbs;
    [SerializeField]
    Color filledIn, empty;
    
    CharacterActionHandler action;
    
    void Start()
    {
        action = characterPanel.Actor.Character.ActionHandler;
        action.RegisterListener(UpdateInfo);
        UpdateInfo();
    }

    void UpdateInfo()
    {
        for (int i = 0; i < apOrbs.Length; i++)
        {
            apOrbs[i].color = i < action.Points ? filledIn : empty;
        }    
    }
}
