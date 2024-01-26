using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Top : MonoBehaviour
{
    float darbegucum;
    int benkimim;

    GameObject gamekontrol;
    GameObject Oyuncu;

    PhotonView pw;
    AudioSource top_yok_olma_ses;

    void Start()
    {
        darbegucum = 20;
        gamekontrol = GameObject.FindWithTag("GameKontrol");

        pw = GetComponent<PhotonView>();
        top_yok_olma_ses=GetComponent<AudioSource>();


    }
    [PunRPC]
    public void TagAktar(string gelentag)
    {
        Oyuncu = GameObject.FindWithTag(gelentag);
        if(gelentag=="Oyuncu_1")
            benkimim=1;
        else 
            benkimim=2;




    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OrtadakiKutular"))
        {
            other.gameObject.GetComponent<PhotonView>().RPC("darbeal",RpcTarget.All,darbegucum);
            Oyuncu.GetComponent<Oyuncum>().poweroynasin();

                PhotonNetwork.Instantiate("duman_puf_carpma_efect", transform.position, transform.rotation, 0, null);
                top_yok_olma_ses.Play();
                if (pw.IsMine)
                    PhotonNetwork.Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Oyuncu_2_kule") || other.gameObject.CompareTag("Oyuncu_2"))
        {
            if(benkimim !=2)
            {
                gamekontrol.GetComponent<PhotonView>().RPC("darbealvur",RpcTarget.All,2, darbegucum);
            
            }

            Oyuncu.GetComponent<Oyuncum>().poweroynasin();
            PhotonNetwork.Instantiate("duman_puf_carpma_efect", transform.position, transform.rotation, 0, null);
            top_yok_olma_ses.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Oyuncu_1_kule") || other.gameObject.CompareTag("Oyuncu_1"))
        {

            if (benkimim != 1)
            {
                 gamekontrol.GetComponent<PhotonView>().RPC("darbealvur",RpcTarget.All,1, darbegucum);
            }

            Oyuncu.GetComponent<Oyuncum>().poweroynasin();
            PhotonNetwork.Instantiate("duman_puf_carpma_efect", transform.position, transform.rotation, 0, null);
            top_yok_olma_ses.Play();
            if (pw.IsMine)                
                PhotonNetwork.Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Zemin"))
        {

            Oyuncu.GetComponent<Oyuncum>().poweroynasin();
            PhotonNetwork.Instantiate("duman_puf_carpma_efect", transform.position, transform.rotation, 0, null);
            top_yok_olma_ses.Play();
            if (pw.IsMine)               
                PhotonNetwork.Destroy(gameObject);

        }
        if (other.gameObject.CompareTag("tahta"))
        {

            Oyuncu.GetComponent<Oyuncum>().poweroynasin();
            PhotonNetwork.Instantiate("duman_puf_carpma_efect", transform.position, transform.rotation, 0, null);
            top_yok_olma_ses.Play();
            if (pw.IsMine)               
                PhotonNetwork.Destroy(gameObject);

        }
         if (other.gameObject.CompareTag("odul"))
        {

            gamekontrol.GetComponent<PhotonView>().RPC("saglikdoldur",RpcTarget.All,benkimim);
            PhotonNetwork.Destroy(other.transform.gameObject);
            Oyuncu.GetComponent<Oyuncum>().poweroynasin();
            PhotonNetwork.Instantiate("duman_puf_carpma_efect", transform.position, transform.rotation, 0, null);
            top_yok_olma_ses.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }
         if (other.gameObject.CompareTag("top"))
        {

            Oyuncu.GetComponent<Oyuncum>().poweroynasin();

            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            top_yok_olma_ses.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }

       
       
       
    }
}
