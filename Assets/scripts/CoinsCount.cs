using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCount : MonoBehaviour
{
    public Text CoinsCountText;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoinsCountText.text = count.ToString();
    }

    public void Addcount()
    {
        count++;
    }
}
