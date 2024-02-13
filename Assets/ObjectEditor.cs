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
    }

    // Update is called once per frame
    void Update()
    {
        //�E�N���b�N��EditMode��؂�ւ�
        if (Input.GetMouseButtonDown(1))
        {
            EditModeManager.instance.ToggleWithRightClick();
            // Moving�t���O������
            if (EditModeManager.instance.isMoving)
            {
                EditModeManager.instance.SetMoving(false);
            }
        }

        //�X�y�[�X�L�[��EditMode��؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EditModeManager.instance.ToggleWithSpace();
            // Moving�t���O������
            if (EditModeManager.instance.isMoving)
            {
                EditModeManager.instance.SetMoving(false);
            }
        }

        // ���݂�EditMode�Ɋ�Â��ăe�L�X�g��\��
        modeText.text = "Mode: " + EditModeManager.instance.currentMode.ToString();

        // �I�����[�h�ō��N���b�N�������ꂽ�Ƃ��ɃI�u�W�F�N�g��I������
        if (EditModeManager.instance.currentMode == EditModeManager.EditMode.Selecting && Input.GetMouseButtonDown(0))
        {
            SelectObjectWithRaycast();
        }
    }

    public void SelectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int itemLayerMask = 1 << LayerMask.NameToLayer("Item");
            if (Physics.Raycast(ray, out hit, 1000, itemLayerMask)) //���C���}�X�N��item���C���̃I�u�W�F�N�g�݂̂Ƀ��C���΂�
            {
                // �f�o�b�O: ���C�L���X�g�̎��o��
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1.0f);

                // �f�o�b�O: ���C�L���X�g���q�b�g�����ꍇ�̃��O
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                // �f�o�b�O: �q�b�g�����I�u�W�F�N�g�̃v���n�u���ƃ��C���[��
                Debug.Log("Prefab name: " + hit.collider.gameObject.name);
                Debug.Log("Layer name: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

                if (hit.collider != null)
                {
                    //Ray�����������I�u�W�F�N�g��I������
                    selectedObject = hit.collider.gameObject;
                    EditModeManager.instance.SetMoving(true); // Moving�t���O�𗧂Ă�


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
                else
                {
                    // �f�o�b�O: ���C�L���X�g���q�b�g���Ȃ������ꍇ�̃��O
                    Debug.Log("Raycast did not hit any object");
                }
            }
        }
    }


        //x�����̃I�u�W�F�N�g�ړ�
        public void MoveObjectX(bool positive)
        {
            // Moving�t���O���������Ă���ꍇ�A�����ŏ������X�L�b�v
            if (!EditModeManager.instance.isMoving) return;

            if (selectedObject != null)
            {
                float direction = positive ? 1f : -1f;
                selectedObject.transform.Translate(Vector3.right * direction, Space.World);
            }
        }

        //z�����̃I�u�W�F�N�g�ړ�
        public void MoveObjectZ(bool positive)
        {
            // Moving�t���O���������Ă���ꍇ�A�����ŏ������X�L�b�v
            if (!EditModeManager.instance.isMoving) return;

            if (selectedObject != null)
            {
                float direction = positive ? 1f : -1f;
                selectedObject.transform.Translate(Vector3.forward * direction, Space.World);
            }
        }


        /*
        public void OnRightButtonDown()
    {
        MoveObjectX(true);
    }

    public void OnLeftButtonDown()
    {
        MoveObjectX(false);
    }

    public void OnUpButtonDown()
    {
        MoveObjectZ(true);
    }

    public void OnDownButtonDown()
    {
        MoveObjectZ(false);
    }*/
}
