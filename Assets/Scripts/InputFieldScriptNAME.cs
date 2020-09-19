using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldScriptNAME : MonoBehaviour
{
    void Awake()
    {
        TMP_InputField inputField = this.GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(delegate {GameObject.FindWithTag("ROOMS_LOGIC").GetComponent<RoomsLogic>().SetRoom(inputField);});
    }
}
