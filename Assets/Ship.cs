using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager'ý kullanabilmek için import ekliyoruz.

public class Ship : MonoBehaviour
{
    Gun[] guns;

    float moveSpeed = 3;

    bool moveUp;
    bool moveDown;
    bool moveRight;
    bool moveLeft;
    bool speedUp;

    bool shoot;

    private bool isDestroyed = false; // Gemi yok olursa true olacak

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            return; // Eðer gemi yoksa, artýk herhangi bir iþlem yapmamaya devam et
        }

        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns)
            {
                gun.Shoot();
            }
        }

        // ESC tuþuna basýldýðýnda menüye dön
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 3;
        }
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;

        if (pos.x <= 1.5f)
        {
            pos.x = 1.5f;
        }

        if (pos.x >= 16f)
        {
            pos.x = 16f;
        }

        if (pos.y >= 9)
        {
            pos.y = 9;
        }

        if (pos.y <= 1)
        {
            pos.y = 1;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
                isDestroyed = true; // Gemi yok olduktan sonra bu flag'i true yapýyoruz

                // Oyun bittiðinde ana menüye dön
                Invoke("GoToMenu", 1f); // 1 saniye sonra menüye git
            }
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            Destroy(gameObject);
            Destroy(destructable.gameObject);
            isDestroyed = true;

            // Oyun bittiðinde ana menüye dön
            Invoke("GoToMenu", 1f);
        }
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); // Sahne ismini "Menu" olarak deðiþtirdik
    }
}
