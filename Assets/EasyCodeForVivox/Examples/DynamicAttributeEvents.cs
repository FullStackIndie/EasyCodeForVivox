using EasyCodeForVivox;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private string cubeName;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [LoginEvent(LoginStatus.LoggingIn)]
        public void PlayerLoggedIn(ILoginSession loginSession)
        {
            cubeName = loginSession.LoginSessionId.Name;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }


    }
}
