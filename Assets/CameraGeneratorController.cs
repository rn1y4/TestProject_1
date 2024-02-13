using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGeneratorController : MonoBehaviour
{
    private Camera maincamera;
    public GameObject generateTarget; //��������^�[�Q�b�g�̃v���n�u
    public Transform generatedObjectsParent; //���������I�u�W�F�N�g���i�[����e�I�u�W�F�N�g
    public ToggleGroup toggleGroup; //ToggletGroup�ւ̎Q��
    public CameraRotateController cameraController; //CameraRotateController�ւ̎Q��
    public Vector3 objectSize = Vector3.one; // ��������I�u�W�F�N�g�̃T�C�Y
    private bool canGenerate = true; //�I�u�W�F�N�g�����\�����䂷��t���O

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
        toggleGroup.gameObject.SetActive(false); //������Ԃł�ToggleGroupe���A�N�e�B�u�ɂ���
    }

    //�E�N���b�N�������ꂽ�Ƃ��̏���
    void CheckRightClick()
    {
        //�����@�\��ToggleGroupe��\����؂�ւ���
        if (Input.GetMouseButtonDown(1))
        {
            // Toggling���[�h�̂Ƃ��ɂ�Toggle��\���A����ȊO�̂Ƃ��ɂ͔�\���ɂ���
            toggleGroup.gameObject.SetActive(EditModeManager.instance.currentMode == EditModeManager.EditMode.Toggling);
            EditModeManager.instance.ToggleWithRightClick();
            cameraController.BackUpMousePosition(); //ToggleGroup�̕\����Ԃ��؂�ւ�鎞�Ƀ}�E�X�ʒu��ۑ�
        }
    }

    //���N���b�N�������ꂽ�Ƃ��̏���
    void CheckLeftClick()
    {
        //��������I�u�W�F�N�g�������I������ĂȂ��ꍇ�A�����ŏ������I������
        if (generateTarget == null) return;

        // Toggle���\������Ă���ꍇ�A�����ŏ������I������
        if (toggleGroup.gameObject.activeSelf) return;



        //���C����Ground�̃I�u�W�F�N�g��݂̂ɃI�u�W�F�N�g�𐶐�����
        if (Input.GetMouseButtonDown(0) && EditModeManager.instance.currentMode == EditModeManager.EditMode.Generating)
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit, 1000, groundLayerMask)) //���C���}�X�N��Ground���C���̃I�u�W�F�N�g�݂̂Ƀ��C���΂�
            {
                int itemLayerMask = 1 << LayerMask.NameToLayer("Item"); // "Item"���C���[�̃}�X�N
                // �����\��̈ʒu�Ɋ��ɑ��̃I�u�W�F�N�g�����݂��Ȃ����Ƃ��m�F
                if (!Physics.CheckBox(hit.point, objectSize * 0.5f, Quaternion.identity, itemLayerMask))
                {
                    // ���̃I�u�W�F�N�g�����݂��Ȃ��ꍇ�̂݃I�u�W�F�N�g�𐶐�
                    GameObject go = Instantiate(generateTarget, generatedObjectsParent); //���������I�u�W�F�N�g�����̐e�I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ���
                    go.name = generateTarget.name;

                    go.transform.position = hit.point;
                    Debug.Log(hit.point);
                    Debug.Log(hit.transform.name);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�E�N���b�N��EditMode��؂�ւ�
        CheckRightClick();

        ObjectEditor.CheckSpaseKeyDown();

        // �������[�h�ō��N���b�N�������ꂽ�Ƃ��ɃI�u�W�F�N�g�𐶐�����
        if (EditModeManager.instance.currentMode == EditModeManager.EditMode.Generating)
        {
            CheckLeftClick();
        }
    }
}