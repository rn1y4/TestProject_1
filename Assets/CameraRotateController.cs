using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotateController : MonoBehaviour
{
    private Vector3 newAngle ;
    private Vector3 lastMousePosition;
    public float y_rotate = 0.4f;
    public float x_rotate = 0.4f;
    public float maxup = 30;
    public float mindown = -80;

    //ToggleGroupオブジェクトを取得
    public GameObject toggleGroupObject;

    // Start is called before the first frame update
    void Start()
    {
        newAngle = this.gameObject.transform.localEulerAngles;
        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //ToggleGroupが表示されている時はCameraRotate操作を無効化
        if (toggleGroupObject.activeSelf) return;

        if (UnityEngine.Cursor.lockState == CursorLockMode.None)
        {
            newAngle.y += ((Input.mousePosition.x - lastMousePosition.x) * y_rotate);
            newAngle.x -= ((Input.mousePosition.y - lastMousePosition.y) * x_rotate);
            // x_rotate, y_rotate -> カメラの移動スピード調整用変数

            // カメラがmixup以上に上を向かず、mindownよりも下を向かないようにする調整
            newAngle.x = Mathf.Min(newAngle.x, maxup);
            newAngle.x = Mathf.Max(newAngle.x, mindown);

            this.gameObject.transform.localEulerAngles = newAngle;
        }

        // マウスポインターが中央から離れすぎた場合
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
}
