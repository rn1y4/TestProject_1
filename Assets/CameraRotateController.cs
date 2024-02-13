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

    private Vector3 oldPos;
    public CameraGeneratorController generatorController; // CameraGeneratorControllerへの参照を追加

    //ToggleGroupオブジェクトを取得
    public GameObject toggleGroupObject;

    // Start is called before the first frame update
    void Start()
    {
        // トグルが表示されている場合、ここで処理を終了する
        if (generatorController.toggleGroup.gameObject.activeSelf) return;
        newAngle = this.gameObject.transform.localEulerAngles;
        lastMousePosition = Input.mousePosition;
    }

    //右クリックされた時のマウス位置を保存
    public void BackUpMousePosition()
    {
        lastMousePosition= Input.mousePosition;
    }

    void UpdateCameraAngle()
    {
        newAngle.y += ((Input.mousePosition.x - lastMousePosition.x) * y_rotate);
        newAngle.x -= ((Input.mousePosition.y - lastMousePosition.y) * x_rotate);
        // x_rotate, y_rotate -> カメラの移動スピード調整用変数

        // カメラがmixup以上に上を向かず、mindownよりも下を向かないようにする調整
        newAngle.x = Mathf.Min(newAngle.x, maxup);
        newAngle.x = Mathf.Max(newAngle.x, mindown);

        this.gameObject.transform.localEulerAngles = newAngle;
    }

    void UpdateCursorLockState()
    {
        // マウスポインターが中央から離れすぎた場合カーソルロックを行う
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

        //カーソルロックされていない場合、マウスポインターがある方向にカメラを向かせる
        if (UnityEngine.Cursor.lockState == CursorLockMode.None)
        {
            UpdateCameraAngle();
        }

        //カーソルロックを行うか判断する
        UpdateCursorLockState();
    }
}
