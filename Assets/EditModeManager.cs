using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EditModeManager : MonoBehaviour
{
    public static EditModeManager instance; // シングルトンのインスタンス
    public bool isSelecting { get; private set; } // 選択モードかどうか

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

    public void ToggleEditMode()
    {
        // 選択モードを切り替える
        isSelecting = !isSelecting;
    }
}