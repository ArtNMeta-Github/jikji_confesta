using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintAnimationController : MonoBehaviour
{
    public GameObject paper;
    public GameObject paper_print;

    public void EnablePaper() => paper.SetActive(true);
    public void DisablePaper() => paper.SetActive(false);
    public void EnablePaper_Print() => paper_print.SetActive(true);
    public void DisablePaper_Print() => paper_print.SetActive(false);
    public void FocusPaper()
    {
        paper.SetActive(true);
        paper_print.SetActive(false);
    }
    public void FocusPrint()
    {
        paper.SetActive(false);
        paper_print.SetActive(true);
    }
}
