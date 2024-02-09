using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    public CameraGeneratorController generatorController; // CameraGeneratorControllerへの参照を追加

    // Update is called once per frame
    void Update()
    {
        //右クリックでEditModeを切り替え
        if (Input.GetMouseButtonDown(1))
        {
            EditModeManager.instance.ToggleWithRightClick();
        }

        //スペースキーでEditModeを切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EditModeManager.instance.ToggleWithSpace();
        }

        // Toggleが表示されている場合または選択モードの場合、ここで処理を終了する
        if (generatorController.toggleGroup.gameObject.activeSelf || EditModeManager.instance.currentMode != EditModeManager.EditMode.Generating) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float height = Input.GetAxis("Height");

        transform.Translate(h, height, v);
    }
}
