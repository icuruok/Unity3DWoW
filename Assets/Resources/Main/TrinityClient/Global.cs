﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Global : MonoBehaviour
{
    public static GameObject login;
    public static GameObject notifyBox;
    public static GameObject realmBox;
    public static GameObject Realm;
    //public static GameObject characterList;
    //public static GameObject characterCreate;
    //public static GameObject Loading;

    static GameObject notify;    
    static GameObject realmList;
    
    static Text NotifyText;
    static Button NotifyButton;
    static Text NotifyButtonText;

    public static Sprite realmListHighlight;
    public static Sprite realmListClear;

    static Text realmName;
    static Text realmTypetext;
    static Text realmCharacterCount;
    static Text realmPop;
    static Image realmSelect;

    public static void showNotifyBox(string error, string button)
    {
        if (!GameObject.Find("NotifyBox"))
        {
            notify = Instantiate(notifyBox, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity);
            notify.transform.parent = GameObject.Find("Canvas").gameObject.transform;
            notify.name = "NotifyBox";
        }

        NotifyText = GameObject.Find("NotifyBoxText").GetComponent<Text>();
        NotifyText.text = error;

        NotifyButtonText = GameObject.Find("NotifyButtonText").GetComponent<Text>();
        NotifyButtonText.text = button;

        NotifyButton = GameObject.Find("NotifyButton").GetComponent<Button>();
        NotifyButton.onClick.AddListener(closeNotify);
    }

    public static void closeNotify()
    {
        Destroy(notify);
    }

    public static void showRealmList(Realm[] realm)
    {
        if (GameObject.Find("NotifyBox"))
        {
            closeNotify();
        }

        if (!GameObject.Find("RealmList"))
        {
            realmList = Instantiate(realmBox, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity);
            realmList.transform.parent = GameObject.Find("Canvas").gameObject.transform;
            realmList.name = "RealmList";

            int space = 0;
            foreach (Realm rl in realm)
            {
                GameObject newgo = Instantiate(Realm, new Vector3(Screen.width / 2, Screen.height / 2 - space, 0), Quaternion.identity);
                newgo.transform.parent = GameObject.Find("RealmList").gameObject.transform;
                newgo.name = rl.Name;

                Transform[] ts = newgo.transform.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in ts)
                {
                    if (t.gameObject.name == "RealmRealmName")
                    {
                        t.gameObject.name = rl.Name + "RealmRealmName";
                    }

                    if (t.gameObject.name == "RealmType")
                    {
                        t.gameObject.name = rl.Name + "RealmType";
                    }

                    if (t.gameObject.name == "RealmCharacters")
                    {
                        t.gameObject.name = rl.Name + "RealmCharacters";
                    }

                    if (t.gameObject.name == "RealmPopulation")
                    {
                        t.gameObject.name = rl.Name + "RealmPopulation";
                    }

                    if (t.gameObject.name == "RealmSelect")
                    {
                        t.gameObject.name = rl.Name + "RealmSelect" + rl.ID;
                    }
                }

                realmName = GameObject.Find(rl.Name + "RealmRealmName").GetComponent<Text>();
                realmName.text = rl.Name;
                if (rl.wOnline == 0)
                { realmName.color = Color.gray; }
                else { realmName.color = Color.green; }

                string realmType = "";
                realmTypetext = GameObject.Find(rl.Name + "RealmType").GetComponent<Text>();

                switch (rl.Type)
                {
                    case 1:
                        realmType = "PvP";
                        break;
                    case 4:
                        realmType = "Normal";
                        break;
                    case 6:
                        realmType = "RP";
                        break;
                    case 8:
                        realmType = "RPPvP";
                        break;
                    case 16:
                        realmType = "FFa_PvP";
                        break;
                    default:
                        realmType = "Normal";
                        break;
                }

                realmTypetext.text = realmType;
                if (rl.wOnline == 0)
                { realmTypetext.color = Color.gray; }
                else { realmTypetext.color = Color.green; }

                realmCharacterCount = GameObject.Find(rl.Name + "RealmCharacters").GetComponent<Text>();
                realmCharacterCount.text = "(" + rl.NumChars.ToString() + ")";
                if (rl.wOnline == 0)
                { realmCharacterCount.color = Color.gray; }
                else { realmCharacterCount.color = Color.green; }

                realmPop = GameObject.Find(rl.Name + "RealmPopulation").GetComponent<Text>();

                string popLevel = "";

                switch (Convert.ToInt32(rl.Population))
                {
                    case 0:
                        popLevel = "Low";
                        break;
                    case 1:
                        popLevel = "Medium";
                        break;
                    case 2:
                        popLevel = "High";
                        break;
                }


                realmPop.text = popLevel;
                if (rl.wOnline == 0)
                {
                    realmPop.color = Color.gray;
                    realmPop.text = "Offline";
                }
                else { realmPop.color = Color.green; }

                space = space + 25;
            }
        }

    }

    public static void selectRealm(GameObject gameObject)
    {
        foreach (Realm rl in Exchange.authClient.Realmlist)
        {
            realmSelect = GameObject.Find(rl.Name + "RealmSelect" + rl.ID).GetComponent<Image>();
            realmSelect.sprite = realmListClear;

        }

        int x = Convert.ToInt32(gameObject.name.Substring(gameObject.name.Length - 1));
        Exchange.currRealm = Exchange.authClient.Realmlist[x];

        if (Exchange.currRealm.wOnline == 0)
        {

        }
        else
        {
            realmSelect = GameObject.Find(gameObject.name).GetComponent<Image>();
            realmSelect.sprite = realmListHighlight;
        }
    }

    public static void closeRealmList()
    {
        Destroy(realmList);
    }
}
