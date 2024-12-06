using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public float scrollSpeed = 40f;     //Adjust the speed of the scroll

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Start the coroutine to return to MainMenu after 43 seconds
        StartCoroutine(ReturnToMainMenuAfterDelay(43f));
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move the text upwards over time
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }

    IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu"); // Load the "MainMenu" scene
    }
}
