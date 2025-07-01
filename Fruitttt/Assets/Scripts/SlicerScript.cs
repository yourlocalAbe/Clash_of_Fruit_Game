using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SlicerScript : MonoBehaviour
{
    private TrailRenderer trail;
    public CapsuleCollider sliceCollider;

    public TextMeshProUGUI scoreTextDisplay;
    public GameObject gameOverPanel;

    public static int score = 0;

    private Camera mainCamera;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        sliceCollider = GetComponent<CapsuleCollider>();

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("ERROR: Main Camera not found or not tagged 'MainCamera'!");
        }
        else
        {
            Debug.Log("AWAKE: Found Main Camera: " + mainCamera.name);
        }

        if (sliceCollider == null)
        {
            Debug.LogError("ERROR: CapsuleCollider still not assigned in Awake after public declaration!");
        }
        else
        {
            Debug.Log("CapsuleCollider successfully found (public reference): " + sliceCollider.gameObject.name);
        }

        trail.emitting = false;
        sliceCollider.enabled = false;
    }

    void Start()
    {
        if (scoreTextDisplay != null)
        {
            scoreTextDisplay.text = "Score: " + score;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Button Held Down!");
            Debug.Log("Raw Input.mousePosition: " + Input.mousePosition);

            if (mainCamera == null)
            {
                Debug.LogError("Main Camera is null in Update!");
                return;
            }

            Debug.Log("Camera position in Update: " + mainCamera.transform.position);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane gamePlane = new Plane(Vector3.forward, Vector3.forward * -1f); 

            float distance;
            if (gamePlane.Raycast(ray, out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                transform.position = mousePos;
                Debug.Log("Slicer moving to world pos (Raycast): " + mousePos); 
            }
            else
            {
                Debug.Log("Ray did not hit the game plane!");
            }

            trail.emitting = true;
            sliceCollider.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Button Released!");
            trail.emitting = false;
            trail.Clear();
            sliceCollider.enabled = false;
        }
        else
        {
            sliceCollider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            FruitScript fruit = other.GetComponent<FruitScript>();
            if (fruit != null)
            {
                fruit.SliceMe(scoreTextDisplay);
            }
        }
        else if (other.CompareTag("Bomb"))
        {
            Debug.Log("GAME OVER! You sliced a BOMB!");
            Destroy(other.gameObject);
            Time.timeScale = 0f;
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SlicerScript.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}