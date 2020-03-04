using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCode : MonoBehaviour {

    public Text bestScoreText;

    private void Start()
    {
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestKey");        
    }

    public void GameStartFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
