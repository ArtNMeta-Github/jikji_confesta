using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    private float timer = 0f;

    public float idleTime = 60f;

    private Vector3 lastPos = Vector3.zero;
    void Update()
    {
        if (Input.anyKey)
        {
            timer = 0f;            
            return;
        }

        if(BNG.InputBridge.Instance.LeftThumbstickAxis != Vector2.zero)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;
        if (timer >= idleTime)
            RestartScene();        
    }

    void RestartScene()
    {    
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
