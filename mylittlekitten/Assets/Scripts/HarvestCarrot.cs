using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class  HarvestCarrot : MonoBehaviour
{
    private Vector3 vector;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool harvestable;
    public bool harvestStart;
    public GameObject hitObject;
    public GameObject harvestMotion;
    public Text progressText;
    public int clickCount;
    AudioManager audioManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMap")
        {
            Destroy(this);
        }

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        GameObject harvestProgressObject = GameObject.Find("HarvestProgress");

        if (harvestProgressObject != null)
        {
            progressText = harvestProgressObject.GetComponent<Text>();
            progressText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            harvestable = false;
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            RaycastHit2D Interactivehit;

            Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;
            Vector2 start = capsuleCollider.bounds.center;
            Vector2 end = start + inputDirection * raycastLength;

            capsuleCollider.enabled = false;
            Interactivehit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawRay(start, inputDirection * raycastLength, Color.green);
            capsuleCollider.enabled = true;

            if (Interactivehit.transform != null)
            {
                harvestable = true;
                hitObject = Interactivehit.collider.gameObject;
            }
            else
            {
                progressText.gameObject.SetActive(false);
                harvestStart = false;
                hitObject = null;
            }
        }

        if (harvestable && Input.GetKeyDown(KeyCode.Z))
        {
            clickCount = 0;
            harvestStart = true;
            audioManager.PlaySound("Harvesting");
        }

        if (harvestStart)
        {
            progressText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                clickCount++;

                float progressPercentage = clickCount / 20.0f * 100.0f;

                progressText.text = progressPercentage.ToString() + "%";
            }

            if (clickCount==20)
            {
                audioManager.PlaySound("Harvest");

                DataManager.Instance.myCarrot++;
                Destroy(hitObject);

                progressText.text = "0%";
                harvestStart=false;
                progressText.gameObject.SetActive(false);
                
                Vector3 harvestVector = new Vector3(33f, 6f+0.5f*(DataManager.Instance.myCarrot - 1), transform.position.z);
                Quaternion carrotRotation = Quaternion.Euler(0f, 0f, 300f);

                GameObject newObject = Instantiate(harvestMotion, harvestVector, carrotRotation);
                print("myCarrot : " + DataManager.Instance.myCarrot);

                harvestable = false;
            }
        }
        else
        {
            if (GameObject.Find("ADHarvesting"))
            {
                Destroy(GameObject.Find("ADHarvesting"));
            }
        }
    }
}

