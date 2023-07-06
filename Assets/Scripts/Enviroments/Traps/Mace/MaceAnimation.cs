using UnityEngine;
using Cinemachine;
using System.Collections;
using DG.Tweening;

public class MaceAnimation : MonoBehaviour
{
    [SerializeField] private float _waitForUIToDisplay;
    [SerializeField] private float _forceSpeed;

    [Space]

    [Header("UI GameObject")]
    [SerializeField] private GameObject _dead_UI;

    [Space]

    [Header("GameObject Properties")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _followedCamera;

    [Space]

    [Header("Animation Properties")]
    [SerializeField] private float _endHeightUp;
    [SerializeField] private float _endHeightDown;
    public float heightOfAnimtion;
    [SerializeField] private bool _isDoneUp = false;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _followedCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update() {
        TrapAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Die());
        }
    }

    private void TrapAnimation()
    {
        if (!_isDoneUp)
        {
            transform.DOMoveY(transform.position.y + heightOfAnimtion, 1);
            if (transform.position.y >= _endHeightUp)
            {
                _isDoneUp = true;
            }
        }

        if (_isDoneUp)
        {
            transform.DOMoveY(transform.position.y - heightOfAnimtion, 1);

            if (transform.position.y <= _endHeightDown)
            {
                _isDoneUp = false;
            }
        }
    }

    IEnumerator Die()
    {
        // Disable Player Collision
        _player.GetComponent<BoxCollider2D>().enabled = false;

        // Disable PlayerController Script
        _player.GetComponent<PlayerController>().enabled = false;

        // Disable MainCamera
        _followedCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;

        // Add force up to the player
        _player.GetComponent<Rigidbody2D>().AddForce(transform.up * _forceSpeed, ForceMode2D.Impulse);

        // Play Dead Sound
        

        // Wait for some amount of second
        yield return new WaitForSecondsRealtime(_waitForUIToDisplay);


        // Display the Dead UI
        _dead_UI.SetActive(true);
        
        Debug.Log("DIE!!!!!!!!!");
    }
}
