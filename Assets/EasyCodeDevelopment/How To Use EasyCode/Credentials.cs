using EasyCodeForVivox;
using System;
using UnityEngine;

public class Credentials : EasyManager
{
    [SerializeField] string apiEndpoint;
    [SerializeField] string domain;
    [SerializeField] string issuer;
    [SerializeField] string secretKey;

    private void Awake()
    {
        EasySession.APIEndpoint = new Uri(apiEndpoint);
        EasySession.Domain = domain;
        EasySession.Issuer = issuer;
        EasySession.SecretKey = secretKey;
    }


}
