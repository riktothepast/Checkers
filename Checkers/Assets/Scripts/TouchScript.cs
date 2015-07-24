using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {

    GameObject currentChecker = null;
    bool locked;
    public LayerMask tilesMask, checkersMask;
    CheckerBoard board;
	// Use this for initialization
	void Start () {
        board = GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckerBoard>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !locked)
        {
            RaycastHit2D hit;
            if(currentChecker)
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 99, tilesMask);
            else
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 99, checkersMask);

            if (hit)
            {
                if (currentChecker && hit.collider.CompareTag("Tile"))
                {
                    if (currentChecker)
                    {
                        if (board.IsPositionAvailable((int)hit.collider.transform.position.x, (int)hit.collider.transform.position.y))
                        {
                            board.SetPositionAvailable((int)currentChecker.transform.position.x, (int)currentChecker.transform.position.y);
                            board.SetPositionUnavailable((int)hit.collider.transform.position.x, (int)hit.collider.transform.position.y);
                            Debug.Log("Moving current checker");
                            currentChecker.GetComponent<Checker>().destination = hit.collider.transform.position;
                            currentChecker.GetComponent<Checker>().shouldMove = true;
                            locked = true;
                        }
                        else 
                        {
                            Debug.Log("cant move there");
                        }
                    }
                }
            }

            if (hit)
            {
                Debug.Log("hit! " + hit.collider.tag);
                if (hit.collider.CompareTag("Checker"))
                {
                    currentChecker = hit.collider.gameObject;
                    Debug.Log("checker selected");
                }
            }
        }
	}

    public void UnlockTouch()
    { 
        locked = false;
        currentChecker = null;
    }
}
