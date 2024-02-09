using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    public CameraGeneratorController generatorController; // CameraGeneratorController�ւ̎Q�Ƃ�ǉ�

    // Update is called once per frame
    void Update()
    {
        //�E�N���b�N��EditMode��؂�ւ�
        if (Input.GetMouseButtonDown(1))
        {
            EditModeManager.instance.ToggleWithRightClick();
        }

        //�X�y�[�X�L�[��EditMode��؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EditModeManager.instance.ToggleWithSpace();
        }

        // Toggle���\������Ă���ꍇ�܂��͑I�����[�h�̏ꍇ�A�����ŏ������I������
        if (generatorController.toggleGroup.gameObject.activeSelf || EditModeManager.instance.currentMode != EditModeManager.EditMode.Generating) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float height = Input.GetAxis("Height");

        transform.Translate(h, height, v);
    }
}
