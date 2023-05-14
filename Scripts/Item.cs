using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameManager gameManager;
    public Enemy enemy;
    public enum Items { LifeUp, TimePause }
    public Items itemType;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            StartCoroutine(DestroyItem());
            switch (itemType)
            {
                case Items.TimePause:
                    enemy.Stun();
                    break;
                case Items.LifeUp:
                    gameManager.health++;
                    break;
            }
        }
    }

    IEnumerator DestroyItem()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.layer = 6;//Dead
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}
