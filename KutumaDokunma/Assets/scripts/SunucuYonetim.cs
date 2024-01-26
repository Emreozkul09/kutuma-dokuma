using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

using System;
using Unity.Mathematics;
using UnityEngine.UI;

public class SunucuYonetim : MonoBehaviourPunCallbacks
{
    
    GameObject serverbilgi;

    GameObject adkaydet;
    GameObject randomgiriss;
    GameObject odakurvegir;
    public bool butonlami;
    
    void Start()
    {
        
        serverbilgi=GameObject.FindWithTag("server_bilgi");
        
        
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }

    public override void OnConnectedToMaster()
    {
        serverbilgi.GetComponent<Text>().text="suncuya baglandi";
        
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
      serverbilgi.GetComponent<Text>().text="lobbye bağlandi";

     
        
    }
      
      public void randomgirisyap()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();

    }
    public void odaolusturvegir()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinOrCreateRoom("538482" ,new RoomOptions{MaxPlayers=2,IsOpen=true,IsVisible=true},TypedLobby.Default);

    }
    public override void OnJoinedRoom()
    {
        InvokeRepeating("bilgilerikontrolet",0,1f);
        GameObject objem=PhotonNetwork.Instantiate("Oyuncu",Vector3.zero,Quaternion.identity,0,null);
        objem.GetComponent<PhotonView>().Owner.NickName=PlayerPrefs.GetString("kullaniciadi");
        
        if(PhotonNetwork.PlayerList.Length==2)
        {
            objem.gameObject.tag="Oyuncu_2";
            GameObject.FindWithTag("GameKontrol").gameObject.GetComponent<PhotonView>().RPC("Basla",RpcTarget.All);

 
        }
    }
    public override void OnLeftRoom()
    {

        if(butonlami)
            {
                Time.timeScale=1;
                PhotonNetwork.ConnectUsingSettings();

            }  
            else{
                Time.timeScale=1;
                PhotonNetwork.ConnectUsingSettings();
                PlayerPrefs.SetInt("toplam_mac",PlayerPrefs.GetInt("toplam_mac")+1);  
                PlayerPrefs.SetInt("mağlubiyet",PlayerPrefs.GetInt("mağlubiyet")+1);
        

            }       
        
       
    }
    public override void OnLeftLobby()
    {
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(butonlami)
            {
                Time.timeScale=1;
                PhotonNetwork.ConnectUsingSettings();

            }  
            else{
                Time.timeScale=1;
                PhotonNetwork.ConnectUsingSettings();
               
                PlayerPrefs.SetInt("toplam_mac",PlayerPrefs.GetInt("toplam_mac"+1));
                PlayerPrefs.SetInt("galibiyet",PlayerPrefs.GetInt("galibiyet"+1));
                PlayerPrefs.SetInt("toplam_puan",PlayerPrefs.GetInt("toplam_puan"+150));

            }    
        
        
        
         InvokeRepeating("bilgilerikontrolet",0,1f);
      
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        serverbilgi.GetComponent<Text>().text="Random bir odaya katilamadi";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
         serverbilgi.GetComponent<Text>().text="Random bir oda olusturulamadi";
    }
    void bilgilerikontrolet(){
        if(PhotonNetwork.PlayerList.Length==2)
        {
            GameObject.FindWithTag("oyuncubekleniyor").SetActive(false);
            GameObject.FindWithTag("oyuncu_1_isim").GetComponent<TextMeshProUGUI>().text=PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("oyuncu_2_isim").GetComponent<TextMeshProUGUI>().text=PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("bilgilerikontrolet");
        }
        else
        {
            GameObject.FindWithTag("oyuncubekleniyor").SetActive(true);
            GameObject.FindWithTag("oyuncu_1_isim").GetComponent<TextMeshProUGUI>().text=PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("oyuncu_2_isim").GetComponent<TextMeshProUGUI>().text=".....";
       

        }

    }




}
