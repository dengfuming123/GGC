using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour {

    private Ctrl ctrl;

    public GameObject skill_0;
    public GameObject skill_1;
    public GameObject skill_2;
    public GameObject skill_3;

    private void Awake()
    {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
    }

    private void Update()
    {
        
    }

}
