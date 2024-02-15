using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static SaveButton;
using UnityEngine.UI;

public class LayoutLoader : MonoBehaviour
{
    public ToggleObjectSelector.ToggleObjectPair[] toggleObjectPairs;  // ���O�ƃv���n�u�̃y�A�̔z��
    private Dictionary<string, GameObject> objectPrefabs; // ���O�ƃv���n�u�̎���
    public Dropdown layoutDropdown;
    public Transform objectsParent; //���������I�u�W�F�N�g���i�[����e�I�u�W�F�N�g

    private void Start()
    {
        // �z�񂩂玫�����쐬
        objectPrefabs = new Dictionary<string, GameObject>();
        foreach (var pair in toggleObjectPairs)
        {
            objectPrefabs.Add(pair.prefab.name, pair.prefab);
        }
    }

    public void LoadLayout()
    {
        //�Z�[�u�f�[�^�ǂݍ���
        string path = Path.Combine(Application.persistentDataPath, layoutDropdown.options[layoutDropdown.value].text + ".json");
        Debug.Log("Loading from path: " + path);

        // �Z�[�u�f�[�^�����݂��Ȃ��ꍇ�̓G���[���b�Z�[�W���o�͂��ď������I��
        if (!File.Exists(path))
        {
            Debug.LogError("LoadLayout: File does not exist: " + path);
            return;
        }

        // �V�������C�A�E�g��ǂݍ��ޑO�ɁA�����̃I�u�W�F�N�g��S�č폜����
        foreach (Transform child in objectsParent)
        {
            Destroy(child.gameObject);
        }

        string json = File.ReadAllText(path);

        //JSON�����X�g�ɕϊ�
        List<ObjectData> objectDatas = JsonUtility.FromJson<Serialization<ObjectData>>(json).ToList();

        //���X�g�̊e�v�f�ɂ���
        foreach (ObjectData data in objectDatas)
        {
            //�I�u�W�F�N�g�̃v���n�u���擾���A�C���X�^���X���쐬
            GameObject prefab = objectPrefabs[data.name];
            GameObject instance = Instantiate(prefab, objectsParent); //���������I�u�W�F�N�g�����̐e�I�u�W�F�N�g�̎q�Ƃ���
            instance.name = data.name;//�C���X�^���X�̖��O���Z�[�u�f�[�^�̖��O�ɐݒ�

            //�C���X�^���X�̈ʒu��ݒ�
            instance.transform.position = data.position;
        }

        Debug.Log("LoadLayout: Successfully loaded the layout.");
    }
}
