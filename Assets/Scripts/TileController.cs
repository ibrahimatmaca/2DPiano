using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {
    // buradan bir tane global bir değişken ile game control objesine tıklanıp tıklanmadığını aktaracağız!
    public bool isBlack;
    public SpriteRenderer mySprite;

    private void Start()
    {
        
        mySprite = transform.GetComponent<SpriteRenderer>();
    }

    public void SetTileStatus(bool _isBlack)
    {//burada isBlack referans değişkenine TilePackController dan gelen değişkenimizi atadık!
        isBlack = _isBlack;
    }

    public void OnMouseDown()
    {
        GameControlScript.instantiate.isBlackControl = isBlack;
        //Debug.Log("Is this black ? -> " + isBlack);
        GameControlScript.instantiate.firstClickDown = true;
        if (isBlack)
        {
            GameControlScript.instantiate.isNowClickControl = isBlack;
            GameControlScript.instantiate.score++;
            mySprite.color = Color.blue;
            isBlack = false;
        }
        else
        {
            GameControlScript.instantiate.gameOver = true;
        }
    }
}
