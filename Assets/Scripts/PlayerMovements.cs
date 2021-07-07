using UnityEngine;
using System.Collections;
using NETWORK_ENGINE;
using UnityEngine.UI;
using System;

public class PlayerMovements : NetworkComponent
{
    public GameObject audioSRC;
    public string username;
    public int score;
    public GameObject gameui;
    public GameObject winUI;
    public GameObject hitbox;
    public GameObject[] crystalList;
    public int maxCrystals;
    public bool isDead;
    public bool winCondition;
    public override void HandleMessage(string flag, string value)
    {

        if (flag == "FOR")
        {
            if (IsServer)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, myrig.velocity.y, 0)
                    + this.transform.forward * float.Parse(value) * 10f;
                SendUpdate("FOR", value);
            }
            if (IsClient)
            {
                if
                   (float.Parse(value) != 0)
                {
                    myanim.SetInteger("Walking", 1);
                }
                else
                {
                    myanim.SetInteger("Walking", 0);
                }
            }

        }
        if (flag == "HOR")
        {
            if (IsServer)
            {
                Vector3 v3 = new Vector3(0f, float.Parse(value), 0f);
                transform.Rotate(v3 * 7f);
            }
        }
        if (flag == "SCORE")
        { 
            if (IsClient)
            {
                audioSRC.GetComponent<AudioManager>().Play("PlayerCollect");
                myanim.Play("attack", 0);
                score = int.Parse(value);
                gameui.transform.GetChild(1).gameObject.GetComponent<Text>().text = value;
            }
        }
        if (flag == "OW")
        {
            if (IsClient)
            {
                audioSRC.GetComponent<AudioManager>().Play("PlayerDie");
                isDead = bool.Parse(value);
            }
        }
        if (flag == "WIN")
        {
            if (IsClient)
            {
                winCondition = bool.Parse(value);
            }
        }
    }


    public override IEnumerator SlowUpdate()
    {
        audioSRC = GameObject.FindGameObjectWithTag("AudioManager");
        myanim = this.gameObject.GetComponent<Animator>();
        myrig = this.gameObject.GetComponent<Rigidbody>();
        if (IsLocalPlayer)
        {
            gameui = GameObject.FindGameObjectWithTag("GameUi");
            winUI = GameObject.FindGameObjectWithTag("Victory");
            gameui.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
            gameui.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
            gameui.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Crystals:";
            gameui.transform.GetChild(2).gameObject.GetComponent<Text>().text = username;
            crystalList = GameObject.FindGameObjectsWithTag("Coin");
            maxCrystals = crystalList.Length;
            gameui.transform.GetChild(4).gameObject.GetComponent<Text>().text = crystalList.Length + " / " + maxCrystals;
        }
        while (true)
        {
            if (IsClient)
            {
            }
            if (IsLocalPlayer)
            {
                if (isDead)
                {
                    GameObject.FindGameObjectWithTag("GameOver").GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0);
                    GameObject.FindGameObjectWithTag("GameOver").GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0);
                }
                else if (winCondition)
                {
                    Debug.Log("We win");
                    winUI.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0);
                    winUI.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0);
                    winUI.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Crystals:";
                    winUI.transform.GetChild(4).gameObject.GetComponent<Text>().text = score.ToString();
                    gameui.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                    gameui.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                }
                else
                {
                    crystalList = GameObject.FindGameObjectsWithTag("Coin");
                    gameui.transform.GetChild(4).gameObject.GetComponent<Text>().text = crystalList.Length + " / " + maxCrystals;
                    //Use input
                    float forward = Input.GetAxisRaw("Vertical");
                    SendCommand("FOR", forward.ToString());

                    float turn = Input.GetAxisRaw("Horizontal");
                    SendCommand("HOR", turn.ToString());


                    gameui.transform.GetChild(2).gameObject.GetComponent<Text>().text = username;
                }
            }
            if (IsServer)
            {
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (IsServer)
        {
            Debug.Log(other.tag);
            if (other.tag == "Coin")
            {
                score++;
                SendUpdate("SCORE", score.ToString());
                MyCore.NetDestroyObject(other.GetComponent<NetworkID>().NetId);
            }
            if (other.tag == "Enemy")
            {
                isDead = true;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 40, this.transform.position.z);
                myrig.constraints = RigidbodyConstraints.FreezePositionY;
                SendUpdate("OW", isDead.ToString());
            }
            if (other.tag == "Gate Switch")
            {
                GameObject.FindGameObjectWithTag("Gate").GetComponent<GateControl>().lockcode++;
                //Play Ready
            }
            if (other.tag == "Exit")
            {
                winCondition = true;
                SendUpdate("WIN", true.ToString());
            }


        }
    }


    Rigidbody myrig;
    Animator myanim;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(IsLocalPlayer & !isDead)
        {

            Vector3 Target = -4f * this.transform.forward + 4f * this.transform.up + this.transform.position;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Target, .75f);

            Camera.main.transform.LookAt(this.transform.GetChild(2).transform);
        }
    }
}
