using UnityEngine;

public class Destructable : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager'� bul
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy) // E�er d��man�n vuruldu�undan eminsek
        {
            Destroy(gameObject); // D��man� yok et
            Destroy(bullet.gameObject); // Mermiyi yok et
            if (gameManager != null)
            {
                gameManager.EnemyDestroyed(); // GameManager'a haber ver
            }
        }
    }
}
