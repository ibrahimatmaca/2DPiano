using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {

    public static GameControlScript instantiate;

    public int score = 0;
    public int columnPoolSize ;
    public float targetPosition; //objemizin gitmesi gereken pozisyon
    private int musicIndex = 0; 
    public int currentColumnIndex = 0; //dizide tuttuğumuz tilepack objesinin 
    public bool movementIsFinish = false;
    public bool firstClickDown = false;
    public bool gameOver = false;
    public bool isBlackControl;
    public bool isNowClickControl;

    public GameObject columnPack;
    public Text scoreText;
    public List<GameObject> columnList = new List<GameObject>();
    private Vector3 createPosition = new Vector3(0, -5, 0);
    public AudioClip[] musicClip;
    public AudioSource musicSource;

    public void Awake()
    {
        instantiate = this;
    }

    private void Start()
    {
        for (int i = 0; i < columnPoolSize; i++)
        { //burada objelerimiz oluşturuluyor!
            columnList.Add(Instantiate(columnPack, createPosition, Quaternion.identity, transform));
            createPosition.y += 2.5f;
        }
    }

    private void FixedUpdate()
    {
        if (isNowClickControl)
        { //Objeye tıklanma 
            if (musicIndex <= musicClip.Length - 1)
            {
                MusicPlay(musicIndex);
                musicIndex++;
            }
            else
            {
                musicIndex = 0;
                MusicPlay(musicIndex);
            }
            isNowClickControl = false;
        }
        if (gameOver)
        {
            if(score > PlayerPrefs.GetInt("BestKey", score))
            {
                PlayerPrefs.SetInt("BestKey", score);
            }
            SceneManager.LoadScene(0);
        }
        scoreText.text = "Score: " + score;
    }

    public void CurrentPositionControl(int index)
    { //Current block position control
        targetPosition = columnList[index].GetComponent<TilePackController>().targetPosition;
        if (columnList[index].transform.position.y == targetPosition)
        {
            if (isBlackControl)
            {
                columnList[index].GetComponent<TilePackController>().CalculateBlackTile();
                PositionTransport(true, index);
            }
            else
            {
                gameOver = true;
            }
        }
        else
        {
            PositionTransport(false, index);
        }
    }

    private void PositionTransport(bool transportControl,int index)
    {
        if (transportControl)
        {
            float position = columnList[columnList.Count - 1].transform.position.y + 2.5f;

            GameObject transport = columnList[index]; //Aktarılacak elemanı bir local objeye atadık     
            columnList.RemoveAt(index);   //gelen index i 1 olan objeyi listeden kaldırdık
            columnList.Add(transport);  //local objedeki elemanı listeye ekledik
            columnList[columnList.LastIndexOf(transport)].transform.position = new Vector3(0, position, 0);
        }
    }
    
    private void MusicPlay(int ındexMusic)
    {
        musicSource.clip = musicClip[ındexMusic];
        musicSource.Play();
    }
}





//private void ColumnMovement() //taşınacak olan kısım
//{ //Code block for the movement of our columns
//    positionNew = columnList[currentColumnIndex].transform.position;
//    positionNew.y = Mathf.MoveTowards(positionNew.y, targetPosition, 0.3f);
//    if(Mathf.Abs(positionNew.y - targetPosition) < 0.01)
//    {
//        positionNew.y = targetPosition; // 
//        movementIsFinish = true;
//    }
//    columnList[currentColumnIndex].transform.position = positionNew; //keeps moving object new Position assignment
//    CurrentPositionControl(currentColumnIndex);
//    currentColumnIndex++;
//    if(currentColumnIndex >= columnPoolSize)
//    {
//        currentColumnIndex = 0;
//    }
//}



//columnList[index].GetComponent<TilePackController>().CalculateBlackTile();
//PositionTransport(true, index);





//private GameObject[] currentColumn;
//currentColumn = new GameObject[columnPoolSize];

//currentColumn[i] = Instantiate(columnPack, createPosition, Quaternion.identity,transform);
//Debug.Log("Create Position: "+i +" "+ createPosition);


//Buradan sonrası PositionTransport ile yer değiştirilebilir
//float transport = currentColumn[currentColumn.Length - 1].transform.position.y;
//if (transportControl)
//{
//    currentColumn[index].transform.position = new Vector3(0,transport+(2.5f*(index+1)),0);
//}