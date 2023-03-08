using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    //private Transform transform;

    // Desired duration of the shake effect
    private float _shakeDuration = 0f;

    public float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    public Transform player;

    private bool shake = false;

    // Update is called once per frame
    void Shake()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            _shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = initialPosition;
            shake = false;
        }
    }

    public void TriggerShake()
    {
        _shakeDuration = shakeDuration;
        shake = true;
    }

    private void Update()
    {
        initialPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        if (shake == false)
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        else
            Shake();
    }
}
