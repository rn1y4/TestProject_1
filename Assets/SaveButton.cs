using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    //���������I�u�W�F�N�g�̐e�I�u�W�F�N�g
    public Transform objectsParent;
    public Dropdown layoutDropdown;

    //�Z�[�u�{�^�����������Ƃ��ɌĂ�
    public void OnClick()
    {
        Debug.Log("SaveButton: OnClick() called.");
        List<ObjectData> objectDatas = new List<ObjectData>();

        //�e�I�u�W�F�N�g�̑S�Ă̎q�I�u�W�F�N�g(���������S�ẴI�u�W�F�N�g)
        foreach (Transform child in objectsParent)
        {
            //���O�ƈʒu���擾���A���X�g�ɒǉ�
            ObjectData data = new ObjectData();
            data.name = child.name;
            data.position = child.position;
            objectDatas.Add(data);
        }

        string json = JsonUtility.ToJson(new Serialization<ObjectData>(objectDatas));

        //���X�g��JSON�t�@�C���ɏ����o��
        string path = Path.Combine(Application.persistentDataPath, layoutDropdown.options[layoutDropdown.value].text + ".json");
        File.WriteAllText(path, json);

        Debug.Log("SaveButton: Successfully saved the layout.");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���X�g��JSON�ŃV���A���C�Y���邽�߂̃��b�p�[�N���X
    [System.Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }
        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }

    // �I�u�W�F�N�g�̖��O�A�ʒu�A��]�A�X�P�[����ێ����邽�߂̃N���X
    [System.Serializable]
    public class ObjectData
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}
