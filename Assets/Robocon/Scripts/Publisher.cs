using RosMessageTypes.Std;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine;

/// <summary>
/// </summary>
public class Publisher : MonoBehaviour
{
    public string topicName = "pos_rot";

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;
    private ROSConnection ros;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    private void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<StringMsg>(topicName);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            var msg = new StringMsg("hello world");
            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, msg);

            timeElapsed = 0;
        }
    }
}