using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleObjectSelector : MonoBehaviour
{
    // Toggle�ƃv���n�u���֘A�t���邽�߂̃N���X��`
    [System.Serializable]
    public class ToggleObjectPair
    {
        public Toggle toggle;
        public GameObject prefab;
        public Image image;
    }

    // Toggle�ƃv���n�u�̃y�A��ۑ����郊�X�g
    public List<ToggleObjectPair> toggleObjectPairs;

    // CameraGeneratorController�X�N���v�g�ւ̎Q��
    public CameraGeneratorController generatorController;

    // Start is called before the first frame update
    void Start()
    {
        // �eToggle�Ƀ��X�i�[��ǉ����AToggle���I���ɂȂ����Ƃ��Ɋ֘A����v���n�u��I������悤�ɂ���
        foreach (var pair in toggleObjectPairs)
        {
            pair.toggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    generatorController.generateTarget = pair.prefab;
                }
                else
                {
                    generatorController.generateTarget = null;
                }
            });

            // �g�O���̃��x�����֘A����v���n�u�̖��O�ɐݒ肷��
            Text label = pair.toggle.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = pair.prefab.name;
            }
        }
    }
}
