using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool GameHasEnded = false;
    [SerializeField] private float restartDelay = 0.5f;

    public void GameEnd()
    {
        if (!GameHasEnded)
        {
            GameHasEnded = true;
            Debug.Log("game over");
            // restart game
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    
}
