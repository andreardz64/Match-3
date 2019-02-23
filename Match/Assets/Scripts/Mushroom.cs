using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    public static GameObject gameManager;
    public static GameManager game;
    RaycastHit2D[] hits;

    // Start is called before the first frame update
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        List<GameObject> toDestroyHorizontal = new List<GameObject>();
        List<GameObject> toDestroyVertical = new List<GameObject>();
        hits = Physics2D.RaycastAll(this.transform.position, Vector3.left, 4f); //This counts the original token
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyHorizontal.Add(hits[i].transform.gameObject);
        }
        //Debug.Log("toRight");
        hits = Physics2D.RaycastAll(this.transform.position + new Vector3(1, 0, 0), Vector3.right, 4f);
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyHorizontal.Add(hits[i].transform.gameObject);
        }

        //Debug.Log("toTop");
        hits = Physics2D.RaycastAll(this.transform.position, Vector3.up, 4f);
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyVertical.Add(hits[i].transform.gameObject);
        }

        //Debug.Log("toBottom");
        hits = Physics2D.RaycastAll(this.transform.position + new Vector3(0, -1, 0), Vector3.down, 4f);
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyVertical.Add(hits[i].transform.gameObject);
        }

        
    }
}
