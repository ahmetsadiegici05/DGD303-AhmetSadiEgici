using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public Vector2 velocity;
    public bool isEnemy = false;

    // Ekranda olup olmadýðýný kontrol eden fonksiyon
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

        // Eðer mermi ekranda deðilse yok et
        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    // Mermi düþmana çarptýðýnda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mermi ekran dýþýnda deðilse ve düþman deðilse vurulmaz
        if (!IsVisible())
            return;

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy != isEnemy)
        {
            // Eðer mermi bir düþmana çarptýysa
            Destructable destructable = collision.GetComponent<Destructable>();
            if (destructable != null && !bullet.isEnemy)
            {
                // Düþman vurulabilir
                Destroy(destructable.gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }
}
