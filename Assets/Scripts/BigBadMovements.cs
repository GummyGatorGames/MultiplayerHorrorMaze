using UnityEngine;
using System.Linq;
using System.Collections;
using NETWORK_ENGINE;
using UnityEngine.UI;

public class BigBadMovements : NetworkComponent
{
    public AudioManager audioManager;
    public string username;
    public int score;
    public GameObject gameui;
    public GameObject winUI;
    public GameObject hitbox;
    public PlayerMovements[] knightList;
    public GameObject[] crystalList;
    public int maxCrystals;
    public bool isLose;
    public bool winCondition;

    public override void HandleMessage(string flag, string value)
    {

        if (flag == "FOR")
        {
            if (IsServer)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, myrig.velocity.y, 0)
                    + this.transform.forward * float.Parse(value) * 40f;
                SendUpdate("FOR", value);
            }
            if (IsClient)
            {
                Debug.Log("Client forward");
                if (float.Parse(value) > 0)
                {
                    Debug.Log("Client forward walking");
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
                transform.Rotate(v3 * 10f);
            }
        }

    }

    public override IEnumerator SlowUpdate()
    {
        myrig = this.gameObject.GetComponent<Rigidbody>();
        myanim = this.gameObject.GetComponent<Animator>();
        if (IsLocalPlayer)
        {
            gameui = GameObject.FindGameObjectWithTag("GameUi");
            winUI = GameObject.FindGameObjectWithTag("Victory");
            gameui.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
            gameui.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
            gameui.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Intruders:";
            gameui.transform.GetChild(2).gameObject.GetComponent<Text>().text = username;
            crystalList = GameObject.FindGameObjectsWithTag("Coin");
            knightList = GameObject.FindObjectsOfType<PlayerMovements>().Where(b => !b.isDead).ToArray();
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
                if (winCondition)
                {
                    gameui.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                    gameui.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                    Debug.Log("We win");
                    winUI.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0);
                    winUI.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0);
                    winUI.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Crystals Lost:";
                    winUI.transform.GetChild(4).gameObject.GetComponent<Text>().text = crystalList.Length.ToString();
                    winUI.transform.GetChild(3).gameObject.GetComponent<Text>().text = "You\nKilled them\nALL";

                } else if (isLose)
                {

                    GameObject templose = GameObject.FindGameObjectWithTag("GameOver");
                    templose.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0);
                    templose.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0);
                    templose.transform.GetChild(1).gameObject.GetComponent<Text>().text = "They\nHave\nESCAPED";
                } else
                {

                    knightList = GameObject.FindObjectsOfType<PlayerMovements>().Where(b => !b.isDead).ToArray();
                    if (GameObject.FindObjectsOfType<PlayerMovements>().Where(b => b.winCondition).ToArray().Length == knightList.Length) isLose = true;
                    if (knightList.Length == 0) winCondition = true;
                    gameui.transform.GetChild(1).gameObject.GetComponent<Text>().text = knightList.Length.ToString();
                    crystalList = GameObject.FindGameObjectsWithTag("Coin");
                    gameui.transform.GetChild(4).gameObject.GetComponent<Text>().text = crystalList.Length + " / " + maxCrystals;
                    gameui.transform.GetChild(4).gameObject.GetComponent<Text>().text = crystalList.Length + " / " + maxCrystals;
                    //Use input
                    float forward = Input.GetAxisRaw("Vertical") / 2;
                    
                    SendCommand("FOR", forward.ToString());

                    float turn = Input.GetAxisRaw("Horizontal") / 2;
                    SendCommand("HOR", turn.ToString());
                }
            }
            if (IsServer)
            {
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }

    }


    Rigidbody myrig;
    Animator myanim;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (IsLocalPlayer)
        {

            Vector3 Target = -8f * this.transform.forward + 12f * this.transform.up + this.transform.position;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Target, .75f);

            Camera.main.transform.LookAt(this.transform.GetChild(2).transform);
        }
    }
}
