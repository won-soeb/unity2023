using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;
    public Shooter shooter;
    public Text targetMark;
    public float rotateSpeed;
    public float stunDelay;

    private bool isMove = true;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (shooter != null && !gameManager.isLoading && isMove)
        {
            Vector3 direction = (shooter.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime));
            rb.velocity = direction * gameManager.enemySpeed;
        }
    }

    private void Update()
    {
        if (gameManager.health <= 0)
        {
            gameManager.GameOver();
        }
        if (gameManager.enemyNumber == -1 && !gameManager.isDead)
        {
            gameManager.LevelClear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            StartCoroutine(DestroyEnemy());
            gameManager.enemyNumber--;
            Debug.Log("clear!");
        }
        if (other.gameObject.tag == "Player")
        {
            //StopCoroutine(Damage());
            gameManager.enemyNumber--;
            gameManager.health--;
            StartCoroutine(Damage());
            Debug.Log("Damage!");
        }
    }

    IEnumerator DestroyEnemy()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.layer = 6;//Dead

        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }

    IEnumerator Damage()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.layer = 6;//Dead
        shooter.GetComponent<ParticleSystem>().Play();
        targetMark.color = Color.red;

        yield return new WaitForSeconds(0.3f);
        targetMark.color = Color.white;
        Destroy(gameObject);
    }

    public IEnumerator Stun()
    {
        isMove = false;
        yield return new WaitForSeconds(stunDelay);
        isMove = true;
    }
}

