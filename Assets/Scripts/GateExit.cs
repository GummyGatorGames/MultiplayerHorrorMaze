using UnityEngine;
using System.Collections;
using NETWORK_ENGINE;

public class GateExit : NetworkComponent
{

    public GameObject[] crystalList;

    public override void HandleMessage(string flag, string value)
    {

    }

    public override IEnumerator SlowUpdate()
    {
        crystalList = GameObject.FindGameObjectsWithTag("Coin");
        while (true)
        {
            if (IsServer)
            {
                crystalList = GameObject.FindGameObjectsWithTag("Coin");
                if (crystalList.Length == 0)
                {
                    Debug.Log(crystalList.Length);
                    this.transform.position = new Vector3(0, 30, 20);
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
    void LateUpdate()
    {

    }
}

