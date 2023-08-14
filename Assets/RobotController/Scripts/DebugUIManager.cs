using TMPro;
using UnityEngine;

namespace RobotController
{
    public class DebugUIManager : MonoBehaviour
    {
        [SerializeField] private LanManager lanManager;
        [SerializeField] private TMP_Text currentIpText;

        private void Start()
        {
            currentIpText.text = $"IP: {lanManager.IpAddress}";
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}