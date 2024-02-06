using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static SaveButton;
using UnityEngine.UI;

public class LayoutLoader : MonoBehaviour
{
    //生成するオブジェクトのプレハブ
    public Dictionary<string, GameObject> objectPrefabs = new Dictionary<string, GameObject>();
    public Dropdown layoutDropdown;
    public Transform objectsParent; //生成したオブジェクトを格納する親オブジェクト

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

            //インスタンスの位置を設定
            instance.transform.position = data.position;
        }

        Debug.Log("LoadLayout: Successfully loaded the layout.");
    }
}