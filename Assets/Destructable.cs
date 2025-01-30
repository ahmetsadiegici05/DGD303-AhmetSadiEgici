using UnityEngine;

public class Destructable : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager'ý bul
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy) // Eðer düþmanýn vurulduðundan eminsek
        {
            Destroy(gameObject); // Düþmaný yok et
            Destroy(bullet.gameObject); // Mermiyi yok et
            if (gameManager != null)
            {
                gameManager.EnemyDestroyed(); // GameManager'a haber ver
            }
        }
    }
}
