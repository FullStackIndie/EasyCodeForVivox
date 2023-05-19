using EasyCodeForVivox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ILWeaving : EasyManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throws()
    {
        var reflectionType = typeof(Exception);
        var ctor = reflectionType.GetConstructor(new Type[] { });

        //var constructorReferenece = 
    }

}
