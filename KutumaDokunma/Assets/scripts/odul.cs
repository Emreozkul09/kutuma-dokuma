using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class odul : MonoBehaviour
{
    PhotonView pw;
    void Start()
    {
        pw=GetComponent<PhotonView>();
        StartCoroutine(yokol());
    }

    IEnumerator yokol(){
        yield return new WaitForSeconds(10f);
        if(pw.IsMine)
        PhotonNetwork.Destroy(gameObject);


    }
    
}
