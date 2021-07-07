using UnityEngine;
using System.Collections;
using NETWORK_ENGINE;

public class BadGuyMovement : NetworkComponent
{
    Vector3 distance;
    public float speed = 25f;
    public int enemyHp;
    private float waitTime;
    public float startWaitTime;
    public Transform[] moveSpots;
    private int currentSpot = 0;

    public override void HandleMessage(string flag, string value)
    {

        if (flag == "ENEMYHIT")
        {
            if (IsClient)
            {
                enemyHp = int.Parse(value);

                
            }
        }
    }

    public override IEnumerator SlowUpdate()
    {
        enemyHp = 3;
        while (true)
        {
            if (IsServer)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveSpots[currentSpot].position.x, transform.position.y, moveSpots[currentSpot].position.z), speed * Time.deltaTime);
                transform.LookAt(new Vector3(moveSpots[currentSpot].position.x, transform.position.y, moveSpots[currentSpot].position.z));
                distance = transform.position - moveSpots[currentSpot].position;
                distance.y = 0;
                if (distance.magnitude < .2f)
                {
                    if (waitTime <= 0)
                    {
                        if (currentSpot == moveSpots.Length-1)
                        {
                            currentSpot = 0;
                        } else
                        {
                            currentSpot++;
                        }
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime*2;
                    }
                }
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsServer)
        {
            if(other.tag == "PlayerHitBox")
            {
                enemyHp--;
                if (enemyHp == 0) MyCore.NetDestroyObject(this.GetComponent<NetworkID>().NetId);
                SendUpdate("ENEMYHIT", enemyHp.ToString());
                MyCore.NetDestroyObject(other.GetComponent<NetworkID>().NetId);
            }
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

