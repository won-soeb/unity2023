using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Invoke("LoadGameScene", 3);
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
