using UnityEngine;
using System.Collections;
using NETWORK_ENGINE;
using UnityEngine.UI;


public class GameCharacter : NetworkComponent
{
    public string Pname;
    public int Color;
    public Text myTextbox;

    public override void HandleMessage(string flag, string value)
    {
        if(flag == "PNAME")
        {
            Pname = value;
            myTextbox.text = value;
        }
    }

    public override IEnumerator SlowUpdate()
    {
        /*if (IsServer) { 
            SimpleSynchronization[] AllPlayers = GameObject.FindObjectOfType<SimpleSynchronization>();
            for (int i = 0; i < AllPlayers.Length; i++)
            {
                if (AllPlayers[i].Owner == Owner)
                {
                    Pname = AllPlayers[i].Pname;
                    SendUpdate("PNAME", Pname);
                }
            }
        }*/

        while(true)
        {
            if (IsServer)
            {
                if (IsDirty)
                {
                    SendUpdate("PNAME", Pname);
                    myTextbox.text = Pname;
                    IsDirty = false;
                }
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
