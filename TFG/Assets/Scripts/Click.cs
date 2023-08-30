using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    bool inside;
    private TMP_Text m_TextComponent;
    public GameObject scoreObject;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        score = 0;
        m_TextComponent = scoreObject.GetComponent<TMP_Text>();
        if (m_TextComponent != null) Debug.Log(m_TextComponent.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && inside == true){
          Debug.Log(score);
          score = score + 5;
          m_TextComponent.text=(""+score);
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        inside = true;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        inside = false;
    }

}
