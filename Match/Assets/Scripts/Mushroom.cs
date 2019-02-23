using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    public static GameObject gameManager;
    public static GameManager game;
    RaycastHit2D[] hits;
    List<GameObject> toDestroyHorizontal;
    List<GameObject> toDestroyVertical;
    // Start is called before the first frame update
    
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

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnDestroy()
    {

        game.DstroyMush = true;
    }
}
