using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public Vector2 velocity;
    public bool isEnemy = false;

    // Ekranda olup olmad���n� kontrol eden fonksiyon
    bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }

    void Update()
    {
        velocity = direction * speed;

        // E�er mermi ekranda de�ilse yok et
        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    // Mermi d��mana �arpt���nda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mermi ekran d���nda de�ilse ve d��man de�ilse vurulmaz
        if (!IsVisible())
            return;

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy != isEnemy)
        {
            // E�er mermi bir d��mana �arpt�ysa
            Destructable destructable = collision.GetComponent<Destructable>();
            if (destructable != null && !bullet.isEnemy)
            {
                // D��man vurulabilir
                Destroy(destructable.gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }
}
