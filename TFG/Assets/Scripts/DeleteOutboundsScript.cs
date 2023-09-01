using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeleteOutboundsScript : MonoBehaviour
{

    public GameObject scoreObject;
    int score;
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {   
        text = scoreObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        score = Int32.Parse(text.text);
        score = score - 5;
        text.text=(""+score);
    }


}
