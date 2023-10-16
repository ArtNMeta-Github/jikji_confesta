using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeInstantiate : MonoBehaviour
{
    public GameObject prefab_Char;
    public TMP_InputField tInput;
    public Transform field;

    float i = 0;

    public void MakeBlock()
    {
        foreach (char c in tInput.text)
        {
            GameObject g = Instantiate(prefab_Char, field);
            g.transform.localPosition += Vector3.right * 0.1f * (i % 10) + Vector3.forward * 0.1f * (int)(i / 10);
            i++;

            g.GetComponentInChildren<TMP_Text>().text = c.ToString();
        }
    }
}