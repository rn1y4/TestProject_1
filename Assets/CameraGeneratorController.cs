using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGeneratorController: MonoBehaviour
{
    private Camera maincamera;
    public GameObject generateTarget;　//生成するターゲットのプレハブ
    public ToggleGroup toggleGroup; //ToggletGroupへの参照
    private bool canGenerate = true; //オブジェクト生成可能か制御するフラグ

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
        toggleGroup.gameObject.SetActive(false); //初期状態ではToggleGroupeを非アクティブにする
    }

    // Update is called once per frame
    void Update()
    {
        //右クリックで生成機能を無効化し、ToggleGroupeを表示する
        if (Input.GetMouseButtonDown(1))
        {
            canGenerate =　!canGenerate;
            toggleGroup.gameObject.SetActive(!toggleGroup.gameObject.activeSelf);
        }

        //生成フラグがfalseの場合、ここで処理を終了する
        if (!canGenerate) return;

        //左クリックでオブジェクトを生成する
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray.origin);
            //Debug.Log(ray.direction);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject go = Instantiate(generateTarget);
                go.transform.position = hit.point;
                Debug.Log(hit.point);
                Debug.Log(hit.transform.name);               
            }
        }


    }
}
