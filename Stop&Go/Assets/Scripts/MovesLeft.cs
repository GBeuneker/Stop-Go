using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovesLeft : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Text>().text = MasterScript.movesLeft.ToString();
        if (MasterScript.movesLeft > 1)
            GetComponent<Text>().color = Color.green;
        else if (MasterScript.movesLeft > 0)
            GetComponent<Text>().color = Color.yellow;
        else
            GetComponent<Text>().color = Color.grey;
    }
}
