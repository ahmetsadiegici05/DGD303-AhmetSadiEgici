using UnityEngine;

public class ExitOnEsc : MonoBehaviour
{
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Game Quit!"); 
        }
    }
}
