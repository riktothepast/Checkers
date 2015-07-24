using UnityEngine;
using System.Collections;

public class Checker : MonoBehaviour {
    public bool isKing = false;
    public int tileType { get; set; }
    public Sprite checkerType0, checkerType1;
    public Vector3 destination;
    public bool shouldMove;
    public float speed = 10f;
    public Owner owner;

    public enum Owner
    { 
        Player,
        IA
    }
	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        if (shouldMove)
            UpdatePosition();
    }

    void UpdatePosition()
    {
        destination.z = 0;
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 0.009f)
        {
            shouldMove = false;
            Camera.main.GetComponent<TouchScript>().UnlockTouch();
        }
    }

}
