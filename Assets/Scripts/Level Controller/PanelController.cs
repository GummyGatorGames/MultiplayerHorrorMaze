using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{

    [System.Serializable]
    public class PlayerCollidedEvent : UnityEvent { }

    public Scene ThisScene;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject helpMenu;
    public GameObject inGameUI;
    public GameObject gameOverUi;
    public GameObject levelselectUI;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void onButtonClick(int choiceButt)
    {
        switch (choiceButt)
        {
            case 0:
                creditsMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                creditsMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                helpMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                helpMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                levelselectUI.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                levelselectUI.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
                break;
            case 1:
                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                levelselectUI.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                levelselectUI.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
                break;
            case 2:
                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                creditsMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                creditsMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                helpMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                helpMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
                break;
            case 3:
                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                helpMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                helpMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                creditsMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                creditsMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
                break;
            case 4:
                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);

                helpMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                helpMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                creditsMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                creditsMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                gameOverUi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 4000, 0);
                break;
            case 5:
                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                levelselectUI.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                levelselectUI.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                

                inGameUI.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 2000f);
                inGameUI.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 2000f);

                break;
            case 6:
                Debug.Log("Quit game");
                Application.Quit();
                break;
            case 7:
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            default:
                break;
        }
    }
}
