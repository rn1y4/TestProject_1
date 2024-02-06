using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGeneratorController: MonoBehaviour
{
    private Camera maincamera;
    public GameObject generateTarget;�@//��������^�[�Q�b�g�̃v���n�u
    public Transform generatedObjectsParent; //���������I�u�W�F�N�g���i�[����e�I�u�W�F�N�g
    public ToggleGroup toggleGroup; //ToggletGroup�ւ̎Q��
    public CameraRotateController cameraController; //CameraRotateController�ւ̎Q��
    private bool canGenerate = true; //�I�u�W�F�N�g�����\�����䂷��t���O
    private int num;

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
            canGenerate = !canGenerate;
            toggleGroup.gameObject.SetActive(!toggleGroup.gameObject.activeSelf);
            cameraController.BackUpMousePosition(); //ToggleGroup�̕\����Ԃ��؂�ւ�鎞�Ƀ}�E�X�ʒu��ۑ�
        }
    }

    //���N���b�N�������ꂽ�Ƃ��̏���
    void CheckLeftClick()
    {
        //���C����Ground�̃I�u�W�F�N�g��݂̂ɃI�u�W�F�N�g�𐶐�����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit, 1000, layerMask)) //���C���}�X�N��Ground���C���̃I�u�W�F�N�g�݂̂Ƀ��C���΂�
            {
                GameObject go = Instantiate(generateTarget, generatedObjectsParent); //���������I�u�W�F�N�g�����̐e�I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ���
                go.name = generateTarget.name+ num;
                num++;
                go.transform.position = hit.point;
                Debug.Log(hit.point);
                Debug.Log(hit.transform.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckRightClick(); //�E�N���b�N�������ꂽ�Ƃ��̏���

        //�����t���O��false�̏ꍇ�A�����ŏ������I������
        if (!canGenerate) return;

        CheckLeftClick(); //���N���b�N�������ꂽ�Ƃ��̏���
    }
}
