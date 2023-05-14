using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject targetMenu;
    public enum GameMenu { Start, Exit }
    public GameMenu MenuType;

    private void Awake()
    {
        gameObject.transform.position = targetMenu.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            StartCoroutine(SelectMenu());
            switch (MenuType)
            {
                case GameMenu.Start:
                    Invoke("LoadGame", 5);
                    break;
                case GameMenu.Exit:
                    Invoke("GameExit", 5);
                    break;
            }  
        }
    }

    IEnumerator SelectMenu()
    {
        targetMenu.GetComponent<MeshRenderer>().enabled = false;
        targetMenu.GetComponent<ParticleSystem>().Play();
        gameManager.isDead = true;
        yield return null;
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void GameExit()
    {
        Application.Quit();
        Debug.Log("Game Exit.");
    }
}