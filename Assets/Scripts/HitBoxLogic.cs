using UnityEngine;
using System.Collections;
using NETWORK_ENGINE;

public class HitBoxLogic : NetworkComponent
{

    public override void HandleMessage(string flag, string value)
    {

    }

    public override IEnumerator SlowUpdate()
    {
        while (true)
        {
            if (IsServer)
            {

                yield return new WaitForSeconds(MyCore.MasterTimer * 2);
                MyCore.NetDestroyObject(this.GetComponent<NetworkID>().NetId);
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

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

