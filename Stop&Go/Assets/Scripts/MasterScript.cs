using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MasterScript : MonoBehaviour
{
    public static int movesLeft;

    public static bool endTurn;
    static List<GameObject> players = new List<GameObject>();
    public static GameObject currentPlayer;
    static int playerIndex = 0;

    // Use this for initialization
    void Awake()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(g);
        }
        currentPlayer = players[playerIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (endTurn)
        {
            if (currentPlayer.GetComponent<PlayerMovement>().AtDestination)
            {
                GameObject.Find("Roll Die").GetComponent<Button>().interactable = true;
                movesLeft = 0;
                endTurn = false;
                currentPlayer.GetComponent<PlayerMovement>().HideSpaces();
                NextPlayer();
            }
        }
    }

    void NextPlayer()
    {
        playerIndex++;
        playerIndex %= players.Count;
        currentPlayer = players[playerIndex];
        Camera.main.GetComponent<CameraFollow>().target = currentPlayer;
    }
}
