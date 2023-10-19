using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrintingTool : BNG.GrabbableEvents
{  
    public CustomRenderTexture rt_front;
    public CustomRenderTexture rt_back;
    Texture2D tex_front;
    Texture2D tex_back;
    public Vector2 size;
    public float distance = 0.1f;

    PrintSnap snap;
    RenderTexture tempTex;
    RaycastHit hit;

    public UnityEngine.UI.Image slider;
    public float sliderValueMult = 2f;
    private int paperLayer;

    private bool printStarted = false;
    private Vector3 lastPrintPos = Vector3.zero;

    

    public override void OnGrab(Grabber grabber)
    {
        GetComponent<Rigidbody>().isKinematic = false;

        base.OnGrab(grabber);
    }

    public override void OnRelease()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        base.OnRelease();
    }

    // Start is called before the first frame update
    void Start()
    {
        rt_front.Initialize();
        rt_back.Initialize();

        slider.fillAmount = 0f;

        paperLayer = 1 << LayerMask.NameToLayer("Paper");
    }

    void UpdateSlider()
    {
        if (printStarted)
        {   
            var delta = Vector3.Distance(lastPrintPos, hit.point);
            slider.fillAmount += Time.deltaTime * delta * sliderValueMult;
        }
        printStarted = true;
        lastPrintPos = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, distance, paperLayer)) {
            //
            UpdateSlider();

            Vector3 eulerAngles = this.transform.eulerAngles;
            eulerAngles.x = 0.0f;
            eulerAngles.z = 0.0f;
            this.transform.eulerAngles = eulerAngles;

            snap = hit.transform.GetComponent<PrintSnap>();

            if (slider.fillAmount > 0.9f && !snap.isPrint)
            {
                snap.isPrint = true;
                snap.IsClear = true;
                snap.SetActiveGrabbable(true);
            }

            // 종이 업데이트

            tex_front = snap.prints[snap.index].front;
            tex_back = snap.prints[snap.index].back;

            if (!(hit.textureCoord.x < size.x / 2 || hit.textureCoord.x >= 1 - size.x / 2 || hit.textureCoord.y < size.y / 2 || hit.textureCoord.y >= 1 - size.y / 2))
            {
                tempTex = new RenderTexture(tex_front.width, tex_front.height, 0);
                Graphics.Blit(tex_back, tempTex, size, hit.textureCoord - size / 2);
                RenderTexture.active = rt_back;
                GL.PushMatrix();
                GL.LoadPixelMatrix(0, 512, 512, 0);
                Graphics.DrawTexture(new Rect((hit.textureCoord.x - size.x / 2) * 512f, (1 - hit.textureCoord.y - size.y / 2) * 512f, size.x * 512f, size.y * 512f), tempTex);
                GL.PopMatrix();
                RenderTexture.active = null;

                Graphics.Blit(tex_front, tempTex, size, new Vector2(1 -hit.textureCoord.x, hit.textureCoord.y) - size / 2);
                RenderTexture.active = rt_front;
                GL.PushMatrix();
                GL.LoadPixelMatrix(0, 512, 512, 0);
                Graphics.DrawTexture(new Rect((1 - hit.textureCoord.x - size.x / 2) * 512f, (1 - hit.textureCoord.y - size.y / 2) * 512f, size.x * 512f, size.y * 512f), tempTex);
                GL.PopMatrix();
                RenderTexture.active = null;
            }
        }
    }
}
