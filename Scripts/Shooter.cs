using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public GameManager gameManager;
    public Enemy enemy;
    public Camera arCamera;
    public GameObject Item;
    public GameObject bullet;
    public ParticleSystem enemyParticle;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float targetZoom;

    private void FixedUpdate()
    {
        gameObject.transform.position = arCamera.transform.position;
        gameObject.transform.rotation = arCamera.transform.rotation;
    }

    private void Update()
    {
        if(!gameManager.isLoading && !gameManager.isDead)
            StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 arCamPosition = arCamera.transform.position;//ar카메라의 위치
            Quaternion arCamRotation = arCamera.transform.rotation;//ar카메라가 바라보는 각도 

            //addforce함수의 발사 방향,각도설정(각도의 합은 쿼터니언의 곱이다)
            Vector3 addforceDir = Vector3.forward;
            Vector3 arCamDir = arCamRotation * addforceDir;
            Quaternion arCamRot = arCamRotation * Quaternion.Euler(90, 0, 0);

            //화면pixel(2560x1440기준)bullet이 날아가는 경우 오차범위 설정 
            float x = (Input.mousePosition.x - 1280) / 2560f * targetZoom;
            float y = (Input.mousePosition.y - 720) / 1440f * targetZoom;
            
            //bullet생성
            GameObject shot = Instantiate(bullet, new Vector3(x, y, 0) + arCamPosition, arCamRot);
            shot.GetComponent<Rigidbody>().AddForce(arCamDir * bulletSpeed, ForceMode.Impulse);//bullet을 지정된 값으로 발사
            yield return new WaitForSeconds(2f);
            Destroy(shot);//2초 후 파괴 
        }
    }
}
