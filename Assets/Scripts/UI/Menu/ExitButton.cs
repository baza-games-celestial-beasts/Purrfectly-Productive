using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : OnClickButton
    {
        protected override void OnClick()
        {
            Debug.Log("On exit");
            Application.Quit(0);
        }
    }
}

