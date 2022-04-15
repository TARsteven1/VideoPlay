using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace xhwart.uisystem
{
    public class UIButtonSender : MonoBehaviour
    {
        public Button button;

        public void click()
        {
            button.onClick.Invoke();
        }
    }
}