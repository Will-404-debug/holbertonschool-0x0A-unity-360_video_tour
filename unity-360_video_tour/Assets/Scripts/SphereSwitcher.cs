using UnityEngine;
using UnityEngine.EventSystems;

public class HotspotTrigger : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject currentSphere;     // The currently active sphere (e.g., LivingRoom)
    public GameObject targetSphere;      // The sphere to activate (e.g., Cantina)
    public Transform playerCamera;       // The VR camera or main camera
    public Transform targetPosition;     // A transform at the center of the target sphere

    public float gazeDuration = 2f;
    private float gazeTimer = 0f;
    private bool isGazing = false;

    void Update()
    {
        if (isGazing)
        {
            gazeTimer += Time.deltaTime;

            if (gazeTimer >= gazeDuration)
            {
                ActivateHotspot();
                ResetGaze();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ActivateHotspot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isGazing = true;
        gazeTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetGaze();
    }

    private void ResetGaze()
    {
        isGazing = false;
        gazeTimer = 0f;
    }

    public void ActivateHotspot()
    {
        if (currentSphere != null) currentSphere.SetActive(false);
        if (targetSphere != null) targetSphere.SetActive(true);

        if (playerCamera != null && targetPosition != null)
        {
            playerCamera.position = targetPosition.position;
            playerCamera.rotation = targetPosition.rotation;
        }

        Debug.Log("Switched to: " + targetSphere.name);
    }
}
