using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEditor : MonoBehaviour
{
    private Camera maincamera;
    public GameObject selectedObject; //�I�𒆂̃I�u�W�F�N�g
    public Material selectedMaterial; //�I�𒆂̃I�u�W�F�N�g�ɓK�p����}�e���A��
    public Material defaultMaterial; //�I���������ɓK�p����}�e���A��

    private bool isSelecting = false; //�I�����[�h����
    // Start is called before the first frame update
    void Start()
    {
        //�X�y�[�X�L�[�őI�����[�h�؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {  
            isSelecting = !isSelecting;
        }

        //�I�����[�h�ō��N���b�N�������ꂽ�Ƃ��ɃI�u�W�F�N�g��I������
        if (isSelecting && Input.GetMouseButton(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //Ray�����������I�u�W�F�N�g��I������
                if(selectedObject != null)
                {
                    //�I������Ă���I�u�W�F�N�g������ꍇ�͑I������������
                    selectedObject.GetComponent<Renderer>().material = defaultMaterial;
                }
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<Renderer>().material = selectedMaterial;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
