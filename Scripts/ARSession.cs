using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSession : MonoBehaviour
{
    public GameObject arSessionOrigin;

    private void Awake()
    {
        gameObject.transform.position = arSessionOrigin.transform.position;
        /*AR Session은 AR어플리케이션이 실행되었을때 GameObject가 가진 transform값을 '한 번' 초기화 한다.
         AR Session Origin은 AR카메라의 시점을 보여주는 Object이다.
        이 코드를 통해 scene이 로드될 때마다 AR공간에 존재하는 GameObject들의 transform값을 초기화 해줄 필요가 있다.
        AR 카메라는 항상 이동하기 때문이다.*/
    }
}
