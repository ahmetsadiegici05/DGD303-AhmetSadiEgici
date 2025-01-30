using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text winText; // UI Text bileþeni (Inspector'da atanacak)
    private int enemyCount; // Kalan düþman sayýsý

    void Start()
    {
        // Sahnedeki düþmanlarý say
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Eðer winText atanmadýysa sahnede bulmaya çalýþ
        if (winText == null)
        {
            GameObject textObject = GameObject.Find("WinText");
            if (textObject != null)
            {
                winText = textObject.GetComponent<Text>();
            }
        }

        // Eðer hala bulunamadýysa hata mesajý göster
        if (winText == null)
        {
            Debug.LogError("WinText atanmadý! Unity Inspector'dan atadýðýnýzdan emin olun.");
            return;
        }

        // Baþlangýçta Win mesajýný gizle
        winText.gameObject.SetActive(false);
    }

    // Düþman yok edildiðinde çaðrýlýr
    public void EnemyDestroyed()
    {
        enemyCount--; // Bir düþman daha yok edildi
        if (enemyCount <= 0) // Eðer tüm düþmanlar yok olduysa
        {
            winText.gameObject.SetActive(true);
            winText.text = "YOU WIN!";
        }
    }
}
