using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    public CameraGeneratorController generatorController; // CameraGeneratorController�ւ̎Q�Ƃ�ǉ�

    // Update is called once per frame
    void Update()
    {
        // Toggle���\������Ă���ꍇ�܂��͑I�����[�h�̏ꍇ�A�����ŏ������I������
        if (generatorController.toggleGroup.gameObject.activeSelf || EditModeManager.instance.currentMode != EditModeManager.EditMode.Generating) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float height = Input.GetAxis("Height");

        transform.Translate(h, height, v);
    }
}
