using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;



public class GameKontrol : MonoBehaviour
{


    [Header("oyuncu saglik islemleri")]
    public Image oyuncu1_healthbar;
    float oyuncu1_saglik = 100;
    public Image oyuncu2_healthbar;
    float oyuncu2_saglik = 100;
    bool basladikmi;
    int limit;
    int olusmasayisi;
    float beklemesuresi;
    public  GameObject [] noktalar;
    GameObject oyuncu1;
    GameObject oyuncu2;

    PhotonView pw;
   bool oyunbittimi=false;


    void Start()
    {
        pw = GetComponent<PhotonView>();
        basladikmi = false;
        limit = 5;
        beklemesuresi = 15f;
    }
    IEnumerator OlusturmayaBasla()
    {
        olusmasayisi=0;

        while (true && basladikmi)
        {
            

            if(limit==olusmasayisi)
                basladikmi=false;


            
            yield return new WaitForSeconds(15);
            int olusandeger=Random.Range(0,5);
            PhotonNetwork.Instantiate("odul",noktalar[olusandeger].transform.position, noktalar[olusandeger].transform.rotation,0,null);
            olusmasayisi++;

        }

    }
    [PunRPC]
    public void Basla()
    {
        if (PhotonNetwork.IsMasterClient)
            basladikmi=true;
            StartCoroutine(OlusturmayaBasla());
           
          

    }
    public void anamenu(){
        GameObject.FindWithTag("sunucuyonetim").GetComponent<SunucuYonetim>().butonlami=true;
        PhotonNetwork.LoadLevel(0);
    }
    public void normalcikis(){
        PhotonNetwork.LoadLevel(0);
    }



    [PunRPC]
    public void darbealvur(int kriter, float darbegucu)
    {
        switch (kriter)
        {
            case 1:
                
                    oyuncu1_saglik -= darbegucu;
                    oyuncu1_healthbar.fillAmount = oyuncu1_saglik / 100;
                    if (oyuncu1_saglik <= 0)
                    {
                        foreach(GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                        {
                            if(objem.gameObject.CompareTag("oyunsonupanel"))
                            {
                            objem.gameObject.SetActive(true);
                            GameObject.FindWithTag("oyunsonubilgi").GetComponent<TextMeshProUGUI>().text="2.oyuncu kazandi";
                            
                            }
                        }
                        kazanan(2);
                        /*
                        oyuncu1=GameObject.FindWithTag("Oyuncu_1");
                        oyuncu2=GameObject.FindWithTag("Oyuncu_2");

                        oyuncu1.GetComponent<PhotonView>().RPC("malup",RpcTarget.All);
                        oyuncu2.GetComponent<PhotonView>().RPC("galip",RpcTarget.All);*/

                       
                    }   

                break;
            case 2:
                
                    oyuncu2_saglik -= darbegucu;
                    oyuncu2_healthbar.fillAmount = oyuncu2_saglik / 100;
                    if (oyuncu2_saglik <= 0)
                    {
                        foreach(GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                        {
                            if(objem.gameObject.CompareTag("oyunsonupanel"))
                            {
                            objem.gameObject.SetActive(true);
                            GameObject.FindWithTag("oyunsonubilgi").GetComponent<TextMeshProUGUI>().text="1.oyuncvu kazandi";
                            
                            }
                        }
                        kazanan(1);
                        /*
                        oyuncu1=GameObject.FindWithTag("Oyuncu_1");
                        oyuncu2=GameObject.FindWithTag("Oyuncu_2");

                        oyuncu1.GetComponent<PhotonView>().RPC("galip",RpcTarget.All);
                        oyuncu2.GetComponent<PhotonView>().RPC("malup",RpcTarget.All);*/
                       

                    }
                break;
        }
    }
    void kazanan(int deger){
        if(!oyunbittimi){
            GameObject.FindWithTag("Oyuncu_1").GetComponent<Oyuncum>().sonuc(deger);
            GameObject.FindWithTag("Oyuncu_2").GetComponent<Oyuncum>().sonuc(deger);
            oyunbittimi=true;

        }
        

    }
    [PunRPC]
    public void saglikdoldur(int hangioyuncu)
    {
        switch (hangioyuncu)
        {
            case 1:
                oyuncu1_saglik += 30;
                if (oyuncu1_saglik >= 100)
                {

                    oyuncu1_saglik = 100;
                    oyuncu1_healthbar.fillAmount = oyuncu1_saglik / 100;

                }
                else
                {
                    oyuncu1_healthbar.fillAmount = oyuncu1_saglik / 100;
                }

                break;
            case 2:
                oyuncu2_saglik += 30;

                if (oyuncu2_saglik >= 100)
                {

                    oyuncu2_saglik = 100;
                    oyuncu2_healthbar.fillAmount = oyuncu2_saglik / 100;

                }
                else
                {
                    oyuncu2_healthbar.fillAmount = oyuncu2_saglik / 100;
                }

                break;
        }

    }


}
