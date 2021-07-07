using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//THIS LINE IS SUPER IMPORTANT
using NETWORK_ENGINE;

public class SimpleSynchronization : NetworkComponent
{
    //synchronized variables
    public int color = 0;
    public int shape = 0;
    public string playerName;
    public bool ready = false;

    public override void HandleMessage(string flag, string value)
    {
        if(flag == "TYPE")
        {
            shape = int.Parse(value);
            if (IsServer)
            {
                SendUpdate("TYPE", shape.ToString());
            }
        }
        if(flag == "MYNAME")
        {
            playerName = value;
            if(IsServer)
            {
                SendUpdate("MYNAME", playerName);
            }
        }
        if(flag == "READY")
        {
            ready = bool.Parse(value);
            if(IsServer)
            {
                SendUpdate("READY", ready.ToString());
            }
        }
    }

    public override IEnumerator SlowUpdate()
    {
        //initialize your class
        //Network Start code would go here.
        if(IsLocalPlayer)
            this.transform.GetChild(0).gameObject.SetActive(true);
        while (true)
        {
            //Game Logic Loop
            if (IsClient)
            {

            }
            if (IsLocalPlayer)
            {
                
            }
            if (IsServer)
            {
                if (IsDirty)
                {
                    //Send all synchronized info.
                    SendUpdate("TYPE", shape.ToString());
                    SendUpdate("MYNAME", playerName);
                    SendUpdate("READY", ready.ToString());
                    IsDirty = false;
                }
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }
    }
    

    public void setType()
    {
        SendCommand("TYPE", this.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Dropdown>().value.ToString());
    }

    public void setName()
    {
        SendCommand("MYNAME", this.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.GetComponent<Text>().text);
    }

    public void setReady()
    {
        SendCommand("READY", (!ready).ToString());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
