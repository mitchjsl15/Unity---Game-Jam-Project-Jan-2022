using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitySceneViewFix : MonoBehaviour
{
    public SpriteRenderer PlayerSprite;

    void Start()
    {
        PlayerSprite.enabled = false; // get rid of player sprite so animations can play
    }

}