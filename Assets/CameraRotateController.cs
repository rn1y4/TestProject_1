using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotateController : MonoBehaviour
{
    private Vector3 newAngle ;
    private Vector3 lastMousePosition;
    public float y_rotate = 0.5f;
    public float x_rotate = 0.5f;
    public float maxup = 30;
    public float mindown = -80;

    //ToggleGroup�I�u�W�F�N�g���擾
    public GameObject toggleGroupObject;

    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        newAngle = this.gameObject.transform.localEulerAngles;
        lastMousePosition = Input.mousePosition;
    }

    //�E�N���b�N���ꂽ���̃}�E�X�ʒu��ۑ�
    public void BackUpMousePosition()
    {
        lastMousePosition= Input.mousePosition;
    }

    void UpdateCameraAngle()
    {
        newAngle.y += ((Input.mousePosition.x - lastMousePosition.x) * y_rotate);
        newAngle.x -= ((Input.mousePosition.y - lastMousePosition.y) * x_rotate);
        // x_rotate, y_rotate -> �J�����̈ړ��X�s�[�h�����p�ϐ�

        // �J������mixup�ȏ�ɏ���������Amindown�������������Ȃ��悤�ɂ��钲��
        newAngle.x = Mathf.Min(newAngle.x, maxup);
        newAngle.x = Mathf.Max(newAngle.x, mindown);

        this.gameObject.transform.localEulerAngles = newAngle;
    }

    void UpdateCursorLockState()
    {
        // �}�E�X�|�C���^�[���������痣�ꂷ�����ꍇ�J�[�\�����b�N���s��
        if (Mathf.Abs(Screen.width / 2 - Input.mousePosition.x) > 50 || Mathf.Abs(Screen.height / 2 - Input.mousePosition.y) > 50)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            lastMousePosition = Input.mousePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ToggleGroup���\������Ă��鎞��CameraRotate����𖳌���
        if (toggleGroupObject.activeSelf) return;


        //�J�[�\�����b�N����Ă��Ȃ��ꍇ�A�}�E�X�|�C���^�[����������ɃJ��������������
        if (UnityEngine.Cursor.lockState == CursorLockMode.None)
        {
            UpdateCameraAngle();
        }

        //�J�[�\�����b�N���s�������f����
        UpdateCursorLockState();
    }
}