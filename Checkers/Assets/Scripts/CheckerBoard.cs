using UnityEngine;
using System.Collections;

public class CheckerBoard : MonoBehaviour {
    public GameObject[,] board;
    public Sprite boardType0, boardType1;
    public Vector2 boardSize;
    public GameObject prefabTile;
    public GameObject prefabChecker;

	// Use this for initialization
	void Start () {
        board = new GameObject[(int)boardSize.x, (int)boardSize.y];
        GenerateEmptyBoard();
        Camera.main.transform.position = new Vector3(boardSize.x / 2 -0.5f,boardSize.y/2, -10);
        GeneratePlayerCheckers();
        GenerateIACheckers();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //sets the tiles of the new checkerboard, based on the board size.
    void GenerateEmptyBoard()
    {
        int spriteType = 0;
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                GameObject tempTile = (GameObject)Instantiate(prefabTile);
                tempTile.transform.position = new Vector2(x, y);

                tempTile.transform.parent = this.transform;
                tempTile.GetComponent<Tile>().occupied = false;

                if (spriteType % 2 == 0)
                {
                    tempTile.GetComponent<SpriteRenderer>().sprite = boardType0;
                    tempTile.GetComponent<Tile>().tileType = 0;
                }
                else
                {
                    tempTile.GetComponent<SpriteRenderer>().sprite = boardType1;
                    tempTile.GetComponent<Tile>().tileType = 1;
                }
                spriteType++;
                board[x, y] = tempTile;
            }
            spriteType++;
        }
    }

    void GeneratePlayerCheckers()
    {
        int checkerType = 0;
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (y % 2 == 0)
                {
                    if (checkerType % 2 == 0)
                    {
                        GameObject tempChecker = (GameObject)Instantiate(prefabChecker);
                        tempChecker.transform.position = new Vector2(x, y);
                        tempChecker.GetComponent<SpriteRenderer>().sprite = tempChecker.GetComponent<Checker>().checkerType0;
                        tempChecker.GetComponent<Checker>().owner = Checker.Owner.Player;
                        SetPositionUnavailable(x, y);
                    }
                }

                if (!(y % 2 == 0))
                {
                    if (!(checkerType % 2 == 0))
                    {
                        GameObject tempChecker = (GameObject)Instantiate(prefabChecker);
                        tempChecker.transform.position = new Vector2(x, y);
                        tempChecker.GetComponent<SpriteRenderer>().sprite = tempChecker.GetComponent<Checker>().checkerType0;
                        tempChecker.GetComponent<Checker>().owner = Checker.Owner.Player;
                        SetPositionUnavailable(x, y);
                    }
                }
            }
            checkerType++;
        }
    }

    void GenerateIACheckers()
    {
        int checkerType = 0;
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = board.GetLength(1) - 3; y < board.GetLength(1); y++)
            {
                if (y % 2 == 0)
                {
                    if (checkerType % 2 == 0)
                    {
                        GameObject tempChecker = (GameObject)Instantiate(prefabChecker);
                        tempChecker.transform.position = new Vector2(x, y);
                        tempChecker.GetComponent<SpriteRenderer>().sprite = tempChecker.GetComponent<Checker>().checkerType1;
                        tempChecker.GetComponent<Checker>().owner = Checker.Owner.IA;
                        SetPositionUnavailable(x, y);
                    }
                }

                if (!(y % 2 == 0))
                {
                    if (!(checkerType % 2 == 0))
                    {
                        GameObject tempChecker = (GameObject)Instantiate(prefabChecker);
                        tempChecker.transform.position = new Vector2(x, y);
                        tempChecker.GetComponent<SpriteRenderer>().sprite = tempChecker.GetComponent<Checker>().checkerType1;
                        tempChecker.GetComponent<Checker>().owner = Checker.Owner.IA;
                        SetPositionUnavailable(x, y);
                    }
                }
            }
            checkerType++;
        }
    }

    public bool IsPositionAvailable(int x, int y)
    {
        return !board[x, y].GetComponent<Tile>().occupied;
    }

    public void SetPositionAvailable(int x, int y)
    {
        board[x, y].GetComponent<Tile>().occupied = false;
    }

    public void SetPositionUnavailable(int x, int y)
    {
        board[x, y].GetComponent<Tile>().occupied = true;
    }
}
