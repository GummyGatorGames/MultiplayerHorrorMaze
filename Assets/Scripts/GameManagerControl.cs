using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NETWORK_ENGINE;

public class GameManagerControl : NetworkComponent
{
    public SimpleSynchronization[] playerList;

    public override void HandleMessage(string flag, string value)
    {
        if(flag == "GAMESTART")
        {
            for(int i = 0; i < playerList.Length; i++)
            {
                playerList[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if(flag == "CHANGE")
        {
            GameObject[] temporary = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < temporary.Length; i++)
            {
                if (temporary[i].GetComponent<PlayerMovements>())
                {
                    temporary[i].GetComponent<PlayerMovements>().username = playerList[i].playerName;

                } else
                {
                    temporary[i].GetComponent<BigBadMovements>().username = playerList[i].playerName;

                }
            }
        }
    }

    public override IEnumerator SlowUpdate()
    {
        if(IsServer)
        {
            yield return new WaitUntil(() => areWeReady());
            SendUpdate("GAMESTART", "1");

            for (int i = 0; i < playerList.Length; i++)
            {
                if(playerList[i].shape == 2)
                {
                    GameObject tempbad = MyCore.NetCreateObject(playerList[i].shape, playerList[i].Owner, new Vector3(2, 25, 145));
                    tempbad.GetComponent<BigBadMovements>().username = playerList[i].playerName; ; //playerList[i].playerName;

                } else
                {
                    GameObject temp = MyCore.NetCreateObject(playerList[i].shape, playerList[i].Owner, new Vector3(Random.Range(-5.0f, 5.0f), 25, Random.Range(-3.0f, 3.0f)));
                    temp.GetComponent<PlayerMovements>().username = playerList[i].playerName; ; //playerList[i].playerName;
                }
            }
            SendUpdate("CHANGE", "1");

            MyId.NotifyDirty();
        }
        if(IsLocalPlayer)
        {
            SendCommand("GAMESTART", "1");
            SendCommand("CHANGE", "1");
        }

        yield return new WaitForSeconds(MyCore.MasterTimer);
    }


    public bool areWeReady()
    {
        if(playerList.Length < 1)
        {
            return false;
        }

        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].ready == false)
                return false;
        }
        return true;
    }

    void Start()
    {
        playerList = GameObject.FindObjectsOfType<SimpleSynchronization>();
    }

    void Update()
    {
        playerList = GameObject.FindObjectsOfType<SimpleSynchronization>();
    }
}
