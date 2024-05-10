using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    [SerializeField] private List<KeyCode> keyCodeList;

    [SerializeField] private List<GameObject> feedbackList;
    // Start is called before the first frame update
    void Update()
    {
        for (int i = 0; i < keyCodeList.Count; i++)
        {
            if (Input.GetKeyDown(keyCodeList[i]))
                feedbackList[i].SetActive(true);
            if (Input.GetKeyUp(keyCodeList[i]))
                feedbackList[i].SetActive(false);
        }
    }
}
