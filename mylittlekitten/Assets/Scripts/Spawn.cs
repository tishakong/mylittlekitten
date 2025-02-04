using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Spawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;
    public string previousScene;
    ExitManager exitManager;

    void Start()
    {
        exitManager = FindObjectOfType<ExitManager>();
        StateUIManager.instance.gameObject.SetActive(true);
        player = Instantiate(charPrefabs[(int)DataManager.Instance.currentCharacter]);
        previousScene = PlayerPrefs.GetString("PreviousScene");
        print(previousScene);
        player.transform.position = new Vector3 (transform.position.x, transform.position.y,0);

        if(exitManager.isRestart==true)
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            exitManager.isRestart = false;
        }

        if (SceneManager.GetActiveScene().name == "MainMap")
        {
            if (string.IsNullOrEmpty(previousScene))
            {
                player.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else if(previousScene == "BakeryShop")
            {
                player.transform.position = new Vector3(-5.5f, 1, 0);
            }
            else if (previousScene == "cafe_black_ver")
            {
                player.transform.position = new Vector3(30, -7, 0);
            }
            else if (previousScene == "cafe_white_ver")
            {
                player.transform.position = new Vector3(3.6f, -2, 0);
            }
            else if (previousScene == "Christmas")
            {
                player.transform.position = new Vector3(14.5f, -13, 0);
            }
            else if (previousScene == "Garden")
            {
                player.transform.position = new Vector3(13, 5, 0);
            }
            else if (previousScene == "Store")
            {
                player.transform.position = new Vector3(-18.6f, -9, 0);
            }

            GameObject mainCamera = Camera.main.gameObject;
            mainCamera.transform.parent = player.transform;
            mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }
}
