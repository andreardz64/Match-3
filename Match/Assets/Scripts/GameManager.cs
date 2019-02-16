using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int count;
    public GameObject[] tokenPrefab = new GameObject[3];
    public static GameManager instance = null;
    private GameObject firstPick = null;
    private GameObject secondPick = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PickToken(GameObject picked)
    {
        if (firstPick == null)
        {
            firstPick = picked;
        }
        else if (secondPick == null)
        {
            if ((firstPick.transform.position - picked.transform.position).magnitude == 1)
            {
                secondPick = picked;
                firstPick.GetComponent<Renderer>().material.color = Color.white;
                bool somethingDestroyed = false;
                bool somethingDestroyed2 = false;
                Vector3 firstPosition = firstPick.transform.position;
                firstPick.transform.position = secondPick.transform.position;
                secondPick.transform.position = firstPosition;

                //Debug.Log("first");
                somethingDestroyed = CheckMatch(firstPick.transform.position, firstPick.tag);
                //Debug.Log("second");
                somethingDestroyed2 = CheckMatch(secondPick.transform.position, secondPick.tag);

                if (somethingDestroyed || somethingDestroyed2)
                {

                }
                else
                {
                    firstPosition = firstPick.transform.position;
                    firstPick.transform.position = secondPick.transform.position;
                    secondPick.transform.position = firstPosition;
                    Debug.Log("That move does not destroy anything.");
                }

                FillBoard();
                firstPick = null;
                secondPick = null;
            }
        }
        else
        {
            Debug.Log("Two are already picked");
        }
    }

    private void GenerateToken(int x, int y)
    {
        Instantiate(tokenPrefab[Random.Range(0, 3)], new Vector3(x, y, 0), Quaternion.identity);
    }

    private void FillBoard()
    {
        for (int x = 0; x < 4; x++)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector3(x, -1, 0), Vector3.up, 10f);
            //Debug.Log("At column " + x + ", hits counter: " + hits.Length);
            for (int y = hits.Length; y < 4; y++)
            {
                //Debug.Log("Generate " + x + ", " + y);
                GenerateToken(x, y);
            }
        }
    }

    private bool CheckMatch(Vector3 position, string tag)
    {
        bool somethingDestroyed = false;
        List<GameObject> toDestroyHorizontal = new List<GameObject>();
        List<GameObject> toDestroyVertical = new List<GameObject>();

        //Debug.Log("toLeft");
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(position, Vector3.left, 2f); //This counts the original token
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyHorizontal.Add(hits[i].transform.gameObject);
        }
        //Debug.Log("toRight");
        hits = Physics2D.RaycastAll(position + new Vector3(1, 0, 0), Vector3.right, 2f);
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyHorizontal.Add(hits[i].transform.gameObject);
        }

        //Debug.Log("toTop");
        hits = Physics2D.RaycastAll(position, Vector3.up, 2f);
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyVertical.Add(hits[i].transform.gameObject);
        }

        //Debug.Log("toBottom");
        hits = Physics2D.RaycastAll(position + new Vector3(0, -1, 0), Vector3.down, 1f);
        for (int i = 0; i < hits.Length && hits[i].transform.gameObject.tag == tag; i++)
        {
            //Debug.Log(hits[i].transform.gameObject.tag);
            toDestroyVertical.Add(hits[i].transform.gameObject);
        }

        if (toDestroyHorizontal.Count >= 3)
        {
            foreach (GameObject token in toDestroyHorizontal)
            {

                Destroy(token);
                somethingDestroyed = true;
                count++;
                hits = Physics2D.RaycastAll(token.transform.position, Vector3.up, 10f);
                for (int i = 0; i < hits.Length; i++)
                {
                    //Debug.Log(hits[i].transform.gameObject.tag);
                    hits[i].transform.position += new Vector3(0, -1, 0);
                }
            }

        }

        if (toDestroyVertical.Count >= 3)
        {
            bool firstBall = false;
            foreach (GameObject token in toDestroyVertical)
            {
                Destroy(token);
                somethingDestroyed = true;
                count++;
                if (firstBall == false)
                {
                    hits = Physics2D.RaycastAll(token.transform.position, Vector3.up, 10f);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        //Debug.Log(hits[i].transform.gameObject.tag);
                        hits[i].transform.position += new Vector3(0, -3, 0);
                        firstBall = true;
                    }
                }

            }
        }

        return somethingDestroyed;
    }

    public bool IsFirstPick() { return firstPick == null && secondPick == null; }

    void OnGUI()
    {
        GUILayout.Label("Total destroyed " + count);
    }


}
