using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EditModeManager : MonoBehaviour
{
    public static EditModeManager instance; // �V���O���g���̃C���X�^���X
    public bool isSelecting { get; private set; } // �I�����[�h���ǂ���

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

    public void ToggleEditMode()
    {
        // �I�����[�h��؂�ւ���
        isSelecting = !isSelecting;
    }
}