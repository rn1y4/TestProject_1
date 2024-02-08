using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleObjectSelector : MonoBehaviour
{
    // Toggleとプレハブを関連付けるためのクラス定義
    [System.Serializable]
    public class ToggleObjectPair
    {
        public Toggle toggle;
        public GameObject prefab;
        public Image image;
    }

    // Toggleとプレハブのペアを保存するリスト
    public List<ToggleObjectPair> toggleObjectPairs;

    // CameraGeneratorControllerスクリプトへの参照
    public CameraGeneratorController generatorController;

    // Start is called before the first frame update
    void Start()
    {
        // 各Toggleにリスナーを追加し、Toggleがオンになったときに関連するプレハブを選択するようにする
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

            // トグルのラベルを関連するプレハブの名前に設定する
            Text label = pair.toggle.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = pair.prefab.name;
            }
        }
    }
}
