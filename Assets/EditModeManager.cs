using UnityEngine;

public class EditModeManager : MonoBehaviour
{
    public static EditModeManager instance; // �V���O���g���̃C���X�^���X

    public enum EditMode // EditMode�̗񋓑̂�ǉ�
    {
        None,
        Selecting,
        Generating,
        Toggling
    }

    public EditMode currentMode { get; private set; } = EditMode.None; // ���݂�EditMode��ۑ�����v���p�e�B
    private EditMode previousMode = EditMode.None; // �O��EditMode��ۑ�����v���p�e�B
    public bool isMoving { get; private set; } = false; // Moving�t���O��ǉ�


    void Awake()
    {
        // �V���O���g���̃C���X�^���X��ݒ�
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
        Debug.Log("isMoving: " + isMoving); // Moving�t���O�̏�Ԃ����O�ɏo��
    }

    // Moving�t���O���������\�b�h��ǉ�
    public void ResetMovingMode()
    {
        isMoving = false;
        Debug.Log("isMoving: " + isMoving); // Moving�t���O�̏�Ԃ����O�ɏo��
    }

    public void ToggleWithRightClick()
    {
        // ���݂̃��[�h��Toggling�Ȃ�ȑO�̃��[�h�ɖ߂��B����ȊO�Ȃ�Toggling�ɐ؂�ւ���
        if (currentMode == EditMode.Toggling || currentMode == EditMode.None)
        {
            currentMode = EditMode.Generating;
        }
        else
        {
            currentMode = EditMode.Toggling;
        }
        isMoving = false; // Moving�t���O������
        Debug.Log("Current mode: " + currentMode); // ���݂�EditMode�����O�ɏo��
    }

    public void ToggleWithSpace()
    {
        if (currentMode != EditMode.Toggling)
        {
            currentMode = currentMode == EditMode.Generating ? EditMode.Selecting : EditMode.Generating;
            ResetMovingMode(); // Moving�t���O������
            Debug.Log("Current mode: " + currentMode); // ���݂�EditMode�����O�ɏo��
        }
    }
}
