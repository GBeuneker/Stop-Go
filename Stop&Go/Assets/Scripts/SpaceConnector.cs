using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceConnector : MonoBehaviour
{
    public GameObject[] neighbourSpaces;
    HashSet<GameObject> adjacentSpaces = new HashSet<GameObject>();
    float width, height;

    // Use this for initialization
    void Awake()
    {
        foreach (GameObject g in neighbourSpaces)
            adjacentSpaces.Add(g);

        width = GetComponent<Collider2D>().bounds.size.y;
        height = GetComponent<Collider2D>().bounds.size.x;

        width = GetComponent<Collider2D>().bounds.size.x;
        height = GetComponent<Collider2D>().bounds.size.y;

        SetAdjacentSpaces();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetAdjacentSpaces()
    {
        List<GameObject> neighbours = new List<GameObject>();

        //The space is vertical
        if ((int)transform.rotation.eulerAngles.z == 90 || (int)transform.rotation.eulerAngles.z == 270)
        {
            //Up
            neighbours.Add(FindSpace(0, height));
            //Down
            neighbours.Add(FindSpace(0, -height));
            //Left upper
            neighbours.Add(FindSpace(-width, height / 2 + width / 2));
            //Right upper
            neighbours.Add(FindSpace(width, height / 2 + width / 2));
            //Left lower
            neighbours.Add(FindSpace(-width, -(height / 2 + width / 2)));
            //Right lower
            neighbours.Add(FindSpace(width, -(height / 2 + width / 2)));
        }
        //The space is horizontal
        else
        {
            //Right
            neighbours.Add(FindSpace(width, 0));
            //Left
            neighbours.Add(FindSpace(-width, 0));
            //Left upper
            neighbours.Add(FindSpace(-(width / 2), height));
            //Right upper
            neighbours.Add(FindSpace(width / 2, height));
            //Left lower
            neighbours.Add(FindSpace(-(width / 2), -height));
            //Right lower
            neighbours.Add(FindSpace(width / 2, -height));
        }

        foreach (GameObject g in neighbours)
        {
            if (g != null)
            {
                if (!adjacentSpaces.Contains(g) && g != gameObject)
                    adjacentSpaces.Add(g);
            }
        }

        //Copy the same neighbours to the public list in the inspector
        neighbourSpaces = new GameObject[adjacentSpaces.Count];
        int i = 0;
        foreach (GameObject g in adjacentSpaces)
        {
            neighbourSpaces[i] = g;
            i++;
        }

    }

    GameObject FindSpace(float xOffset, float yOffset)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(xOffset, yOffset, 0), Vector2.zero);

        if (hit.transform == null)
            return null;

        if (hit.transform.tag == "Space")
            return hit.transform.gameObject;
        else
            return null;
    }

    public bool Contains(GameObject gameObject)
    {
        return adjacentSpaces.Contains(gameObject);
    }
}
