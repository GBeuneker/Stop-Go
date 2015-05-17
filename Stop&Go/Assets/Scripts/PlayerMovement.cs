using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    Queue<Vector3> destinationQueue = new Queue<Vector3>();
    float threshold = 1;
    public GameObject currentSpace;
    public LayerMask spaceLayer;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationQueue.Count > 0)
        {
            Vector3 difference = new Vector3(destinationQueue.Peek().x - transform.position.x, destinationQueue.Peek().y - transform.position.y, 0);
            transform.position += difference * Time.deltaTime * 10f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, destinationQueue.Peek().z);
            if (difference.magnitude <= 0.1f)
            {
                //If we reached the destination, remove it from the queue
                destinationQueue.Dequeue();
                //If we are at our goal, hightlight the correct neighbours
                if (destinationQueue.Count <= 0)
                {
                    ShowSpaces();
                }
            }

        }
        if (MasterScript.currentPlayer == gameObject)
            HandleInput();
    }

    void MoveTo(Vector2 destination)
    {
        MasterScript.movesLeft--;
        //We go from horizontal to vertical
        if (Mathf.Abs(destination.y - transform.position.y) > threshold && Horizontal)
        {
            if (FacingRight)
            {
                //We're going right
                if (destination.x - transform.position.x >= threshold)
                {
                    destinationQueue.Enqueue(new Vector3(destination.x, transform.position.y, 0));
                    //We're going up
                    if (destination.y - transform.position.y >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 90));
                    //We're going down
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 270));
                }
                //We're going left
                else
                {
                    MasterScript.movesLeft--;
                    destinationQueue.Enqueue(new Vector3(destination.x, transform.position.y, 0));
                    //We're going up
                    if (destination.y - transform.position.y >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 270));
                    //We're going down
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 90));
                }
            }
            else
            {
                //We're going right
                if (destination.x - transform.position.x >= threshold)
                {
                    MasterScript.movesLeft--;
                    destinationQueue.Enqueue(new Vector3(destination.x, transform.position.y, 180));
                    //We're going up
                    if (destination.y - transform.position.y >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 270));
                    //We're going down
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 90));
                }
                //We're going left
                else
                {
                    destinationQueue.Enqueue(new Vector3(destination.x, transform.position.y, 180));
                    //We're going up
                    if (destination.y - transform.position.y >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 90));
                    //We're going down
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 270));
                }
            }
        }
        //We go from vertical to horizontal
        else if (Mathf.Abs(destination.x - transform.position.x) > threshold && !Horizontal)
        {
            if (FacingUp)
            {
                //We're going Up
                if (destination.y - transform.position.y >= threshold)
                {
                    destinationQueue.Enqueue(new Vector3(transform.position.x, destination.y, 90));
                    //We're going right
                    if (destination.x - transform.position.x >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 0));
                    //We're going left
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 180));
                }
                //We're going down
                else
                {
                    MasterScript.movesLeft--;
                    destinationQueue.Enqueue(new Vector3(transform.position.x, destination.y, 90));
                    //We're going right
                    if (destination.x - transform.position.x >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 180));
                    //We're going left
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 0));
                }
            }
            else
            {
                //We're going Up
                if (destination.y - transform.position.y >= threshold)
                {
                    MasterScript.movesLeft--;
                    destinationQueue.Enqueue(new Vector3(transform.position.x, destination.y, 270));
                    //We're going right
                    if (destination.x - transform.position.x >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 180));
                    //We're going left
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 0));
                }
                //We're going down
                else
                {
                    destinationQueue.Enqueue(new Vector3(transform.position.x, destination.y, 270));
                    //We're going right
                    if (destination.x - transform.position.x >= threshold)
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 0));
                    //We're going left
                    else
                        destinationQueue.Enqueue(new Vector3(destination.x, destination.y, 180));
                }
            }
        }
        else
        {
            destinationQueue.Enqueue(new Vector3(destination.x, destination.y, transform.eulerAngles.z));
            if (FacingRight && destination.x < transform.position.x)
                MasterScript.movesLeft--;
            else if (FacingLeft && destination.x > transform.position.x)
                MasterScript.movesLeft--;
            else if (FacingUp && destination.y < transform.position.y)
                MasterScript.movesLeft--;
            else if (FacingDown && destination.y > transform.position.y)
                MasterScript.movesLeft--;
        }
    }

    public void ShowSpaces()
    {
        if (currentSpace == null || MasterScript.movesLeft <= 0 || MasterScript.endTurn)
            return;
        HideSpaces();
        CheckSpace();
        foreach (GameObject g in currentSpace.GetComponent<SpaceConnector>().neighbourSpaces)
        {
            if (MasterScript.movesLeft < 2)
            {
                if (FacingUp)
                {
                    if (g.transform.position.y < transform.position.y)
                        continue;
                }
                else if (FacingDown)
                {
                    if (g.transform.position.y > transform.position.y)
                        continue;
                }
                else if (FacingRight)
                {
                    if (g.transform.position.x < transform.position.x)
                        continue;
                }
                else if (FacingLeft)
                {
                    if (g.transform.position.x > transform.position.x)
                        continue;
                }
            }

            g.GetComponent<SpaceBehaviour>().Highlighted = true;
        }
    }

    public void HideSpaces()
    {
        if (currentSpace == null)
            return;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Space"))
        {
            g.GetComponent<SpaceBehaviour>().Highlighted = false;
        }
    }

    void HandleInput()
    {
        //Select a space to move to
        if (Input.GetMouseButtonDown(0))
        {
            if (MasterScript.movesLeft <= 0)
                return;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Space")
                {
                    if (hit.transform.GetComponent<SpaceBehaviour>().Highlighted)
                    {
                        MoveTo(hit.transform.position);
                        HideSpaces();
                    }
                }
            }
        }
    }

    public void CheckSpace()
    {
        int test = LayerMask.NameToLayer("Space");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 10, spaceLayer);

        if (hit.collider != null)
        {
            currentSpace = hit.transform.gameObject;
        }
    }

    public bool Horizontal
    {
        get
        {
            return (int)transform.eulerAngles.z == 0 || (int)transform.eulerAngles.z == 180;
        }
    }

    public bool FacingRight
    {
        get
        {
            return (int)transform.eulerAngles.z == 0;
        }
    }

    public bool FacingUp
    {
        get
        {
            return (int)transform.eulerAngles.z == 90;
        }
    }

    public bool FacingLeft
    {
        get
        {
            return (int)transform.eulerAngles.z == 180;
        }
    }


    public bool FacingDown
    {
        get
        {
            return (int)transform.eulerAngles.z == 270;
        }
    }

    public bool AtDestination
    {
        get { return destinationQueue.Count <= 0; }
    }
}
