using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static SaveButton;
using UnityEngine.UI;

public class LayoutLoader : MonoBehaviour
{
    public ToggleObjectSelector.ToggleObjectPair[] toggleObjectPairs;  // 名前とプレハブのペアの配列
    private Dictionary<string, GameObject> objectPrefabs; // 名前とプレハブの辞書
    public Dropdown layoutDropdown;
    public Transform objectsParent; //生成したオブジェクトを格納する親オブジェクト

    private void Start()
    {
        // 配列から辞書を作成
        objectPrefabs = new Dictionary<string, GameObject>();
        foreach (var pair in toggleObjectPairs)
        {
            objectPrefabs.Add(pair.prefab.name, pair.prefab);
        }
    }

    public void LoadLayout()
    {
        //セーブデータ読み込み
        string path = Path.Combine(Application.persistentDataPath, layoutDropdown.options[layoutDropdown.value].text + ".json");
        Debug.Log("Loading from path: " + path);

        // セーブデータが存在しない場合はエラーメッセージを出力して処理を終了
        if (!File.Exists(path))
        {
            Debug.LogError("LoadLayout: File does not exist: " + path);
            return;
        }

        // 新しいレイアウトを読み込む前に、既存のオブジェクトを全て削除する
        foreach (Transform child in objectsParent)
        {
            Destroy(child.gameObject);
        }

        string json = File.ReadAllText(path);

        //JSONをリストに変換
        List<ObjectData> objectDatas = JsonUtility.FromJson<Serialization<ObjectData>>(json).ToList();

        //リストの各要素について
        foreach (ObjectData data in objectDatas)
        {
            //オブジェクトのプレハブを取得し、インスタンスを作成
            GameObject prefab = objectPrefabs[data.name];
            GameObject instance = Instantiate(prefab, objectsParent); //生成したオブジェクトを特定の親オブジェクトの子とする
            instance.name = data.name;//インスタンスの名前をセーブデータの名前に設定

            //インスタンスの位置を設定
            instance.transform.position = data.position;
        }

        Debug.Log("LoadLayout: Successfully loaded the layout.");
    }
}
