using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEditor : MonoBehaviour
{
    private Camera maincamera;
    public GameObject selectedObject; //�I�𒆂̃I�u�W�F�N�g
    public Text selectedObjectNameText; // �I�𒆂̃I�u�W�F�N�g�̖��O��\������e�L�X�g
    public Text modeText; // �I�����[�h��\������e�L�X�g

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();

        if (modeText != null)
        {
            modeText.text = "Mode: Normal";
        }
        else
        {
            Debug.LogError("modeText is not set");
        }

        if (selectedObjectNameText != null)
        {
            selectedObjectNameText.text = "Selected: None";
        }
        else
        {
            Debug.LogError("selectedObjectNameText is not set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[�őI�����[�h�؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleEditMode();
        }

        // �I�����[�h�ō��N���b�N�������ꂽ�Ƃ��ɃI�u�W�F�N�g��I������
        if (EditModeManager.instance.isSelecting && Input.GetMouseButtonDown(0))
        {
            SelectObjectWithRaycast();
        }
    }

    void ToggleEditMode()
    {
        EditModeManager.instance.ToggleEditMode();

        // �I�����[�h�̕\�����X�V
        if (modeText != null)
        {
            modeText.text = EditModeManager.instance.isSelecting ? "Mode: Selecting" : "Mode: Normal";
            Debug.Log("Updated modeText: " + modeText.text);
        }
        else
        {
            Debug.LogError("modeText is not set");
        }
    }

    void SelectObjectWithRaycast()
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int itemLayerMask = 1 << LayerMask.NameToLayer("Item");
        if (Physics.Raycast(ray, out hit, 1000, itemLayerMask)) //���C���}�X�N��item���C���̃I�u�W�F�N�g�݂̂Ƀ��C���΂�
        {
            // �f�o�b�O: ���C�L���X�g�̎��o��
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2.0f);

            // �f�o�b�O: ���C�L���X�g���q�b�g�����ꍇ�̃��O
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            // �f�o�b�O: �q�b�g�����I�u�W�F�N�g�̃v���n�u���ƃ��C���[��
            Debug.Log("Prefab name: " + hit.collider.gameObject.name);
            Debug.Log("Layer name: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            if (hit.collider != null)
            {
                //Ray�����������I�u�W�F�N�g��I������
                selectedObject = hit.collider.gameObject;

                // �I�𒆂̃I�u�W�F�N�g�̖��O���e�L�X�g�ŕ\��
                if (selectedObjectNameText != null)
                {
                    selectedObjectNameText.text = "Selected: " + selectedObject.name;
                    Debug.Log("Updated selectedObjectNameText: " + selectedObjectNameText.text);
                }
                else
                {
                    Debug.LogError("selectedObjectNameText is not set");
                }
            }
        }
        else
        {
            // �f�o�b�O: ���C�L���X�g���q�b�g���Ȃ������ꍇ�̃��O
            Debug.Log("Raycast did not hit any object");
        }
    }
}