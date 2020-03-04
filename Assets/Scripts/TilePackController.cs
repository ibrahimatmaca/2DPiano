using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePackController : MonoBehaviour {

    public float targetPosition;
    public int blackTileVal;
    public GameObject[] tileObjects;
    private Vector3 positionNew;

    private void Start()
    {
        CalculateBlackTile();
    }

    private void FixedUpdate()
    {
        if (GameControlScript.instantiate.firstClickDown)
        {
            if (GameControlScript.instantiate.movementIsFinish == false)
            {
                ColumnMovement();
            }
            else
            {
                GameControlScript.instantiate.movementIsFinish = false;
            }
        }
    }

    public void CalculateBlackTile()
    { // bu fonksiyon her çağırışımzda bize rastgele bir siyah olan eleman veriyor
        blackTileVal = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            if (i == blackTileVal)
            {
                tileObjects[i].GetComponent<TileController>().SetTileStatus(true);
                tileObjects[i].GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                tileObjects[i].GetComponent<TileController>().SetTileStatus(false);
                tileObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void ColumnMovement()
    {
        GameControlScript.instantiate.columnList[GameControlScript.instantiate.currentColumnIndex] = transform.gameObject;
        positionNew = transform.position;
        positionNew.y = Mathf.MoveTowards(positionNew.y, targetPosition, 0.07f);
        if (Mathf.Abs(positionNew.y - targetPosition) < 0.01)
        {
            positionNew.y = targetPosition;
            GameControlScript.instantiate.movementIsFinish = true;
        }
        transform.position = positionNew;
        int index = GameControlScript.instantiate.currentColumnIndex++;
        GameControlScript.instantiate.CurrentPositionControl(index);

        if (GameControlScript.instantiate.currentColumnIndex >= GameControlScript.instantiate.columnPoolSize)
        {
            GameControlScript.instantiate.currentColumnIndex = 0;
        }

    }
}
