using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public NETWORK_ENGINE.NetworkCore myCore;
    // Start is called before the first frame update
    void Start()
    {
        myCore = GameObject.FindObjectOfType<NETWORK_ENGINE.NetworkCore>();
        StartCoroutine(Panels());
    }

    public IEnumerator Panels()
    {
        yield return new WaitUntil(() => myCore.IsConnected);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitWhile(() => myCore.IsConnected);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
