using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Roll()
    {
        GameObject.Find("Die").GetComponent<DieScript>().Roll();
        DisableButton();
    }

    public void DisableButton()
    {
        GetComponent<Button>().interactable = false;
    }

    public void EndTurn()
    {
        MasterScript.endTurn = true;
        DisableButton();
    }
}
