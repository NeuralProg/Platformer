using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgEnemy : BaseCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        direction = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        direction = 0f;
    }
}
