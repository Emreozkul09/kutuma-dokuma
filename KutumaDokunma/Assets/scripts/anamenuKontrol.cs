using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class anamenuKontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ilkpanel;
    public GameObject ikincipanel;
    public InputField kullaniciadi;
    public Text varolankullaniciadi;
    public TextMeshProUGUI [] istatistik;
    public Text serberbilgi;
    void Start()
    {
        if(!PlayerPrefs.HasKey("kullaniciadi")){
            

            PlayerPrefs.SetInt("toplam_mac",0);
            PlayerPrefs.SetInt("galibiyet",0);
            PlayerPrefs.SetInt("mağlubiyet",0);
            PlayerPrefs.SetInt("toplam_puan",0);
            ilkpanel.SetActive(true);
            degeryaz();
        }
        else{
            ikincipanel.SetActive(true);
            varolankullaniciadi.text=PlayerPrefs.GetString("kullaniciadi");
            degeryaz();
        }
    }

    
    
    public void kullaniciadikaydet(){
        PlayerPrefs.SetString("kullaniciadi",kullaniciadi.text);
        ilkpanel.SetActive(false);
        ikincipanel.SetActive(true);
        varolankullaniciadi.text=kullaniciadi.text;

    }
    void degeryaz(){
        istatistik[0].text=PlayerPrefs.GetInt("toplam_mac").ToString();
        istatistik[1].text=PlayerPrefs.GetInt("galibiyet").ToString();
        istatistik[2].text=PlayerPrefs.GetInt("mağlubiyet").ToString();
        istatistik[3].text=PlayerPrefs.GetInt("toplam_puan").ToString();


    }
}
