using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text winText; // UI Text bile�eni (Inspector'da atanacak)
    private int enemyCount; // Kalan d��man say�s�

    void Start()
    {
        // Sahnedeki d��manlar� say
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // E�er winText atanmad�ysa sahnede bulmaya �al��
        if (winText == null)
        {
            GameObject textObject = GameObject.Find("WinText");
            if (textObject != null)
            {
                winText = textObject.GetComponent<Text>();
            }
        }

        // E�er hala bulunamad�ysa hata mesaj� g�ster
        if (winText == null)
        {
            Debug.LogError("WinText atanmad�! Unity Inspector'dan atad���n�zdan emin olun.");
            return;
        }

        // Ba�lang��ta Win mesaj�n� gizle
        winText.gameObject.SetActive(false);
    }

    // D��man yok edildi�inde �a�r�l�r
    public void EnemyDestroyed()
    {
        enemyCount--; // Bir d��man daha yok edildi
        if (enemyCount <= 0) // E�er t�m d��manlar yok olduysa
        {
            winText.gameObject.SetActive(true);
            winText.text = "YOU WIN!";
        }
    }
}
