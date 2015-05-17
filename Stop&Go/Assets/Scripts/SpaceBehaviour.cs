using UnityEngine;
using System.Collections;

public class SpaceBehaviour : MonoBehaviour
{
    public Color highlightColor;
    public LayerMask playerLayer;
    bool highlighted;

    // Use this for initialization
    void Start()
    {
        OriginalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Highlighted
    {
        get { return highlighted; }
        set
        {
            highlighted = value;
            if (highlighted)
            {
                GetComponent<SpriteRenderer>().color = highlightColor;
                if (CarOnSpace != null)
                    GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
                GetComponent<SpriteRenderer>().color = OriginalColor;
        }
    }

    public Color OriginalColor
    {
        get;
        set;
    }

    public GameObject CarOnSpace
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 10, playerLayer);
            if (hit)
            {
                return hit.transform.gameObject;
            }

            return null;
        }
    }
}
