using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Text;

public class Oyuncum : MonoBehaviour
{

    Image powerbar1;

    public GameObject top;
    public GameObject topcikis;
    public ParticleSystem topatisefect;
    public AudioSource Top_atma_sesi;
    bool sonageldimi = false;
    float powersayi;
    float Atisyonu;
    bool atesaktifmi=false;


    Coroutine PowerDongu;

    PhotonView pw;

    void Start()
    {
        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            powerbar1 = GameObject.FindWithTag("mask").GetComponent<Image>();



            if (PhotonNetwork.IsMasterClient)
            {
                gameObject.tag="Oyuncu_1";
                transform.position = GameObject.FindWithTag("olusacaknokta_1").transform.position;
                transform.rotation = GameObject.FindWithTag("olusacaknokta_1").transform.rotation;
                Atisyonu = 2f;

            }
            else
            {
                gameObject.tag="Oyuncu_2";
                transform.position = GameObject.FindWithTag("olusacaknokta_2").transform.position;
                transform.rotation = GameObject.FindWithTag("olusacaknokta_2").transform.rotation;
                Atisyonu = -2f;


            }

        }
        InvokeRepeating("oyunbasladimi", 0, .5f);

    }
    public void oyunbasladimi()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {
                PowerDongu = StartCoroutine(PowerBarCalistir());
                CancelInvoke("oyunbasladimi");

            }
            

        }
        else
        {
            StopAllCoroutines();
        }


    }
    IEnumerator PowerBarCalistir()
    {


        powerbar1.fillAmount = 0;
        sonageldimi = false;
        atesaktifmi=true;

        while (true)
        {
            if (powerbar1.fillAmount < 1 && !sonageldimi)
            {

                powersayi = 0.1f;
                powerbar1.fillAmount += powersayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);

            }
            else
            {
                sonageldimi = true;
                powersayi = 0.1f;
                powerbar1.fillAmount -= powersayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
                if (powerbar1.fillAmount == 0)
                {
                    sonageldimi = false;
                }

            }
        }

    }

    void Update()
    {
        if (pw.IsMine)
        {
            if (Input.touchCount > 0 && atesaktifmi)
            {
                PhotonNetwork.Instantiate("Patlama_efect", topcikis.transform.position, topcikis.transform.rotation, 0, null);


                Top_atma_sesi.Play();
                GameObject topobjem = PhotonNetwork.Instantiate("Top", topcikis.transform.position, topcikis.transform.rotation, 0, null);


                topobjem.GetComponent<PhotonView>().RPC("TagAktar", RpcTarget.All, gameObject.tag);
                Rigidbody2D rg = topobjem.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(Atisyonu, 0f) * powerbar1.fillAmount * 10f, ForceMode2D.Impulse);
                atesaktifmi=false;
                StopCoroutine(PowerDongu);
            }

        }



    }
    public void poweroynasin()
    {

        PowerDongu = StartCoroutine(PowerBarCalistir());

    }


    public void sonuc(int deger)
    {
        if (pw.IsMine)
        {


            if (PhotonNetwork.IsMasterClient)
            {
                if (deger == 1)
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac" + 1));
                    PlayerPrefs.SetInt("galibiyet", PlayerPrefs.GetInt("galibiyet" + 1));
                    PlayerPrefs.SetInt("toplam_puan", PlayerPrefs.GetInt("toplam_puan" + 150));
                    
                }
                else
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);
                    PlayerPrefs.SetInt("mağlubiyet", PlayerPrefs.GetInt("mağlubiyet") + 1);
                    


                }
            }
            else
            {


                if (deger == 2)
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac" + 1));
                    PlayerPrefs.SetInt("galibiyet", PlayerPrefs.GetInt("galibiyet" + 1));
                    PlayerPrefs.SetInt("toplam_puan", PlayerPrefs.GetInt("toplam_puan" + 150));
                    

                }
                else
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);
                    PlayerPrefs.SetInt("mağlubiyet", PlayerPrefs.GetInt("mağlubiyet") + 1);
                    


                }

            }

        }
        Time.timeScale=0;
    }


}



