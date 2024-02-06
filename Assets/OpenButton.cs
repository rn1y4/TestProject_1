using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    public LayoutLoader layoutLoader;

    public void OnClick()
    {
        layoutLoader.LoadLayout();
    }

}
