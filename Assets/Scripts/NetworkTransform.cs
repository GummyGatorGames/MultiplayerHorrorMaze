using UnityEngine;
using System.Collections;
using NETWORK_ENGINE;

public class NetworkTransform : NetworkComponent
{

    public Vector3 LastPosition = Vector3.zero;
    public Vector3 LastRotation = Vector3.zero;

    public override void HandleMessage(string flag, string value)
    {
        //(x,y,z)
        char[] remove = { '(', ')' };
        if (flag == "POS")
        {
            string[] data = value.Trim(remove).Split(',');
            


            Vector3 target = new Vector3(
                    float.Parse(data[0]),
                    float.Parse(data[1]),
                    float.Parse(data[2])
                );

            if((target-this.transform.position).magnitude < .5f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, target, .25f);
            } else
            {
                this.transform.position = target;
            }
        }
        if (flag == "ROT")
        {

            string[] data = value.Trim(remove).Split(',');

            Vector3 euler = new Vector3(
                    float.Parse(data[0]),
                    float.Parse(data[1]),
                    float.Parse(data[2]));
            this.transform.rotation = Quaternion.Euler(euler);
        }
    }

    public override IEnumerator SlowUpdate()
    {
        while (IsServer)
        {
            //is position different
            if(LastPosition != this.transform.position)
            {
                //sendupdate
                SendUpdate("POS", this.transform.position.ToString());
                LastPosition = this.transform.position;
            }
            //is rotation different
            if (LastRotation != this.transform.rotation.eulerAngles)
            {
                //sendupdate
                SendUpdate("ROT", this.transform.rotation.eulerAngles.ToString());
                LastRotation = this.transform.rotation.eulerAngles;
            }
            if (IsDirty)
            {

                SendUpdate("POS", this.transform.position.ToString());
                SendUpdate("ROT", this.transform.rotation.eulerAngles.ToString());
                IsDirty = false;
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
