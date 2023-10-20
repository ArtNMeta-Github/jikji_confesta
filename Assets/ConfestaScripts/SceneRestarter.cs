using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    private float timer = 0f;

    public float idleTime = 60f;
    void Update()
    {
        if (Input.anyKey)
        {
            timer = 0f;
            Debug.Log("Input");
            return;
        }

        timer += Time.deltaTime;
        if (timer >= idleTime)
            RestartScene();

        Debug.Log(timer);
    }

    void RestartScene()
    {    
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
