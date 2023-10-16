using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANM_CustomPlate : MonoBehaviour
{
    [SerializeField] List<Transform> Basic_typeObjs;

    //////////  Getter & Setter //////////
    public List<Transform> ANM_Basic_typeObjs   { get { return Basic_typeObjs;  }   }

    //////////  Method          //////////
    // �ж��� �ξ� ������Ű�� ����.
    public void ANM_Basic_TypeObjsLock()
    {
        for(int i = 0; i < Basic_typeObjs.Count; i++)
        {
            Basic_typeObjs[i].parent.GetComponent<BoxCollider>().enabled = false;
            if (Basic_typeObjs[i].parent.GetComponent<Rigidbody>() != null)
            {
                Destroy(Basic_typeObjs[i].parent.GetComponent<Rigidbody>());
            }
        }
    }

    //////////  Unity           //////////
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision _collision)
    {
        bool isInsert = false;

        // ������Ʈ�� �̹� ����ִ��� ���� üũ
        for(int i = 0; i < Basic_typeObjs.Count; i++)
        {
            if(_collision.transform.GetChild(0).Equals(Basic_typeObjs[i]))
            {
                isInsert = true;
                break;
            }
        }

        // ������Ʈ�� ����Ʈ�� ������� �ʴٸ� �߰�.
        if(!isInsert)
        {
            Basic_typeObjs.Add(_collision.transform.GetChild(0));
        }
    }

    private void OnCollisionExit(Collision _collision)
    {
        Basic_typeObjs.Remove(_collision.transform.GetChild(0));
    }
}
