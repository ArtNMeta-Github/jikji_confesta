﻿using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
using TMPro;

[Serializable]
public struct PrintSet
{
    public GameObject plate;
    public Texture2D front;
    public Texture2D back;
}

// 종이가 인새판 위에 닿았을 때, 인쇄판의 위에 위치하도록 강제시키는 클래스.
public partial class PrintSnap : BNG.GrabbableEvents
{
    public List<PrintSet> prints;

    [HideInInspector]
    public bool isPrint = false;
    [HideInInspector]
    public int index = 0;

    //

    public AudioSource snapAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate") && !isPrint)
        {
            snapAudio.Play();

            foreach (PrintSet p in prints)
            {
                if (p.plate == other.gameObject)
                {
                    index = prints.IndexOf(p);

                    // 밀랍을 붓는 작업이지만 우성 이곳에 먼저 배치합니다.
                    switch (p.plate.name)
                    {
                        case "Plate_Custom":
                            {
                                p.plate.transform.Find("Quad").GetComponent<ANM_CustomPlate>().ANM_Basic_TypeObjsLock();
                            }
                            break;
                    }

                    transform.SetPositionAndRotation(prints[index].plate.transform.position + Vector3.up * Basic_plateUp, Quaternion.identity);

                    switch (p.plate.name)
                    {
                        case "Plate_Custom":
                            {
                                ANM_Custom_OnTriggerEnter(p.plate.transform.Find("Quad").GetComponent<ANM_CustomPlate>());
                            }
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Plate") && !isPrint)
        {
            transform.SetPositionAndRotation(prints[index].plate.transform.position + Vector3.up * Basic_plateUp, Quaternion.identity);
        }
    }
}

// 황영재 추가작업 분량
partial class PrintSnap
{
    [SerializeField] float Basic_plateUp;

    ////////// Unity    //////////
    void ANM_Custom_OnTriggerEnter(ANM_CustomPlate _customPlate)
    {
        Transform words = this.transform.Find("Words");

        while (words.childCount > 0)
        {
            Transform wordChild = words.GetChild(0);

            wordChild.SetParent(null);
            Destroy(wordChild.gameObject);
        }

        //
        List<Transform> typeObjs = _customPlate.ANM_Basic_typeObjs;

        for (int i = 0; i < typeObjs.Count; i++)
        {
            //
            GameObject element = Instantiate(typeObjs[i].gameObject);
            element.GetComponent<TMP_Text>().color = new Color(0, 0, 0, 0.1f);

            element.transform.position      = typeObjs[i].position;
            element.transform.rotation      = typeObjs[i].rotation;
            element.transform.localScale    = typeObjs[i].lossyScale;

            element.transform.SetParent(words);
            element.transform.localPosition = new Vector3(  element.transform.localPosition.x,  0.0001f,    element.transform.localPosition.z   );

            //
            element = Instantiate(typeObjs[i].gameObject);
            element.GetComponent<TMP_Text>().color = new Color(0, 0, 0);

            element.transform.position      = typeObjs[i].position;
            element.transform.rotation      = typeObjs[i].rotation;
            element.transform.localScale    = typeObjs[i].lossyScale;

            element.transform.SetParent(words);
            element.transform.localPosition = new Vector3(  element.transform.localPosition.x,  -0.0001f,   element.transform.localPosition.z   );
        }
    }
}