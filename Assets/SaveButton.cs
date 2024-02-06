using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    //生成したオブジェクトの親オブジェクト
    public Transform objectsParent;
    public Dropdown layoutDropdown;

    //セーブボタンを押したときに呼ぶ
    public void OnClick()
    {
        Debug.Log("SaveButton: OnClick() called.");
        List<ObjectData> objectDatas = new List<ObjectData>();

        //親オブジェクトの全ての子オブジェクト(生成した全てのオブジェクト)
        foreach (Transform child in objectsParent)
        {
            //名前と位置を取得し、リストに追加
            ObjectData data = new ObjectData();
            data.name = child.name;
            data.position = child.position;
            objectDatas.Add(data);
        }

        string json = JsonUtility.ToJson(new Serialization<ObjectData>(objectDatas));

        //リストをJSONファイルに書き出し
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

    // リストをJSONでシリアライズするためのラッパークラス
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

    // オブジェクトの名前、位置、回転、スケールを保持するためのクラス
    [System.Serializable]
    public class ObjectData
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}
