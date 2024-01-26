using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class OrtadakiKutu : MonoBehaviour
{


    public GameObject canvassaglik;
    float saglik = 100;
    public Image healthbar;


    GameObject gamekontrol;
    
    PhotonView pw;
    AudioSource Kutu_yok_olma_ses;

    void Start()
    {
        
        gamekontrol = GameObject.FindWithTag("GameKontrol");
        pw = GetComponent<PhotonView>();


        Kutu_yok_olma_ses=GetComponent<AudioSource>();

    }




    [PunRPC]
    public void darbeal(float darbegucu)
    {
        if (pw.IsMine)
        {
            saglik -= darbegucu;
            healthbar.fillAmount = saglik / 100;
            if (saglik <= 0)
            {

                //gamekontrol.GetComponent<GameKontrol>().SesVeEfectOlustur(2,gameObject);

                PhotonNetwork.Instantiate("Kutu_Patlatma_efect", transform.position, transform.rotation, 0, null);
                Kutu_yok_olma_ses.Play();
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                StartCoroutine(canvascikar());
            }
        }
    }
    IEnumerator canvascikar()
    {
        if (!canvassaglik.activeInHierarchy)
        {
            canvassaglik.SetActive(true);
            yield return new WaitForSeconds(2);
            canvassaglik.SetActive(false);


        }
    }
}
