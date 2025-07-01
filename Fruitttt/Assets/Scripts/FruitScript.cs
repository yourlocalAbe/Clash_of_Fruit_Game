// This is the code for FruitScript.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Needed for scoreTextDisplay reference
using UnityEngine.SceneManagement;

public class FruitScript : MonoBehaviour // <--- This is the class name
{
    public GameObject half1Prefab;
    public GameObject half2Prefab;
    public int scoreValue = 1;
    public float cutForce = 5f;

    public void SliceMe(TextMeshProUGUI scoreDisplay)
    {
        SlicerScript.score += scoreValue;
        if (scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + SlicerScript.score;
        }

        GameObject half1 = Instantiate(half1Prefab, transform.position, Quaternion.identity);
        GameObject half2 = Instantiate(half2Prefab, transform.position, Quaternion.identity);

        Rigidbody rbHalf1 = half1.GetComponent<Rigidbody>();
        Rigidbody rbHalf2 = half2.GetComponent<Rigidbody>();

        Vector3 randomDir1 = Random.insideUnitSphere.normalized;
        Vector3 randomDir2 = -randomDir1;

        rbHalf1.AddForce(randomDir1 * cutForce, ForceMode.Impulse);
        rbHalf2.AddForce(randomDir2 * cutForce, ForceMode.Impulse);

        rbHalf1.AddTorque(Random.insideUnitSphere * cutForce * 0.5f, ForceMode.Impulse);
        rbHalf2.AddTorque(Random.insideUnitSphere * cutForce * 0.5f, ForceMode.Impulse);

        Destroy(gameObject);
    }
}