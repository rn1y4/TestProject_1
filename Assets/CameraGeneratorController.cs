using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGeneratorController: MonoBehaviour
{
    private Camera maincamera;
    public GameObject generateTarget;　//生成するターゲットのプレハブ
    public Transform generatedObjectsParent; //生成したオブジェクトを格納する親オブジェクト
    public ToggleGroup toggleGroup; //ToggletGroupへの参照
    public CameraRotateController cameraController; //CameraRotateControllerへの参照
    private bool canGenerate = true; //オブジェクト生成可能か制御するフラグ
    private int num;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
        toggleGroup.gameObject.SetActive(false); //初期状態ではToggleGroupeを非アクティブにする
    }

    //右クリックが押されたときの処理
    void CheckRightClick()
    {
        //生成機能とToggleGroupeを表示を切り替える
        if (Input.GetMouseButtonDown(1))
        {
            canGenerate = !canGenerate;
            toggleGroup.gameObject.SetActive(!toggleGroup.gameObject.activeSelf);
            cameraController.BackUpMousePosition(); //ToggleGroupの表示状態が切り替わる時にマウス位置を保存
        }
    }

    //左クリックが押されたときの処理
    void CheckLeftClick()
    {
        //レイヤがGroundのオブジェクト上のみにオブジェクトを生成する
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit, 1000, layerMask)) //レイヤマスクでGroundレイヤのオブジェクトのみにレイを飛ばす
            {
                GameObject go = Instantiate(generateTarget, generatedObjectsParent); //生成したオブジェクトを特定の親オブジェクトの子オブジェクトにする
                go.name = generateTarget.name+ num;
                num++;
                go.transform.position = hit.point;
                Debug.Log(hit.point);
                Debug.Log(hit.transform.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckRightClick(); //右クリックが押されたときの処理

        //生成フラグがfalseの場合、ここで処理を終了する
        if (!canGenerate) return;

        CheckLeftClick(); //左クリックが押されたときの処理
    }
}
