using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text winText; 
    private int enemyCount; 

    void Start()
    {
        
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        
        if (winText == null)
        {
            GameObject textObject = GameObject.Find("WinText");
            if (textObject != null)
            {
                winText = textObject.GetComponent<Text>();
            }
        }

       
        if (winText == null)
        {
            Debug.LogError("WinText atanmadý! Unity Inspector'dan atadýðýnýzdan emin olun.");
            return;
        }

        
        winText.gameObject.SetActive(false);
    }

    
    public void EnemyDestroyed()
    {
        enemyCount--; 
        if (enemyCount <= 0) 
        {
            winText.gameObject.SetActive(true);
            winText.text = "YOU WIN!";
        }
    }
}
