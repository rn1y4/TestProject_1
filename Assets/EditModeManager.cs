using UnityEngine;

public class EditModeManager : MonoBehaviour
{
    public static EditModeManager instance; // シングルトンのインスタンス

    public enum EditMode // EditModeの列挙体を追加
    {
        None,
        Selecting,
        Generating,
        Toggling
    }

    public EditMode currentMode { get; private set; } = EditMode.None; // 現在のEditModeを保存するプロパティ
    private EditMode previousMode = EditMode.None; // 前のEditModeを保存するプロパティ
    public bool isMoving { get; private set; } = false; // Movingフラグを追加


    void Awake()
    {
        // シングルトンのインスタンスを設定
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMoving(bool value)
    {
        isMoving = value;
        Debug.Log("isMoving: " + isMoving); // Movingフラグの状態をログに出力
    }

    // Movingフラグを下すメソッドを追加
    public void ResetMovingMode()
    {
        isMoving = false;
        Debug.Log("isMoving: " + isMoving); // Movingフラグの状態をログに出力
    }

    public void ToggleWithRightClick()
    {
        // 現在のモードがTogglingなら以前のモードに戻す。それ以外ならTogglingに切り替える
        if (currentMode == EditMode.Toggling || currentMode == EditMode.None)
        {
            currentMode = EditMode.Generating;
        }
        else
        {
            currentMode = EditMode.Toggling;
        }
        isMoving = false; // Movingフラグを下す
        Debug.Log("Current mode: " + currentMode); // 現在のEditModeをログに出力
    }

    public void ToggleWithSpace()
    {
        if (currentMode != EditMode.Toggling)
        {
            currentMode = currentMode == EditMode.Generating ? EditMode.Selecting : EditMode.Generating;
            ResetMovingMode(); // Movingフラグを下す
            Debug.Log("Current mode: " + currentMode); // 現在のEditModeをログに出力
        }
    }
}
