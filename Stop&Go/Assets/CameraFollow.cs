using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float delay;

    // Use this for initialization
    void Start()
    {
        target = MasterScript.currentPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * delay * new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
    }
}
