using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [Header("Transform References")]
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Head Bobbing")]
    public float bobFrequency = 5f;
    public float bobHorizontalAmp = 0.1f;
    public float bobVerticalAmp = 0.1f;
    [Range(0f, 1f)] public float headBobSmoothing = 0.1f;

    //State
    public bool isWalking;
    private float walkingTime;
    private Vector3 targetCameraPos;

    private void Update()
    {
        if (!isWalking) walkingTime = 0;
        else walkingTime += Time.deltaTime;

        targetCameraPos = headTransform.position + CalculateHeadBobOffset(walkingTime);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPos, headBobSmoothing);
        if ((cameraTransform.position - targetCameraPos).magnitude <= 0.001) cameraTransform.position = targetCameraPos;
    }
    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;
        if(t > 0)
        {
            horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizontalAmp;
            verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmp;

            offset = headTransform.right *horizontalOffset + headTransform.up * verticalOffset;
        }
        return offset;
    }
}
