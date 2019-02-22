using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

    public static GameObject gameManager;
    public static GameManager game;
	// Use this for initialization
	void Start () {
		
	}

    protected void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        game = gameManager.GetComponent<GameManager>();
    }

    protected void OnMouseDown()
    {
        SelectToken();
    }

    public void SelectToken()
    {
        if (game.IsFirstPick())
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
        game.PickToken(gameObject);
    }
}
