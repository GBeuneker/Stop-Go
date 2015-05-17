using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DieScript : MonoBehaviour
{
    public Sprite[] dieSides;
    public float rollDelay;

    float rollTime, timer;
    int dieIndex = 0;
    bool rollQuickly;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rollTime > 0 && rollQuickly)
        {
            rollTime -= Time.deltaTime;
            timer += Time.deltaTime;

            if (timer >= rollDelay)
            {
                timer = 0;
                dieIndex++;
                dieIndex %= dieSides.Length;
                GetComponent<Image>().sprite = dieSides[dieIndex];
            }

            if (rollTime <= 0)
            {
                rollQuickly = false;
                rollTime = 1f;
            }
        }
        else if(rollTime > 0)
        {
            rollTime -= Time.deltaTime;
            timer += Time.deltaTime;
            if(timer >= rollDelay * 3)
            {
                timer = 0;
                dieIndex++;
                dieIndex %= dieSides.Length;
                GetComponent<Image>().sprite = dieSides[dieIndex];
            }
            if (rollTime <= 0)
            {
                MasterScript.movesLeft = dieIndex + 1;
                PlayerMovement playerMovement = MasterScript.currentPlayer.GetComponent<PlayerMovement>();
                playerMovement.ShowSpaces();
                GameObject.Find("End Turn").GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Roll()
    {
        dieIndex = Random.Range(0, dieSides.Length);
        rollTime = Random.Range(1.5f, 2.5f);
        rollQuickly = true;
    }
}
