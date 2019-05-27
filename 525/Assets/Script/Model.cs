using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    [HideInInspector]
    public int skillNum = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            skillNum = Random.Range(1, 5);//生成随机数，随机释放技能
        }
    }
}
