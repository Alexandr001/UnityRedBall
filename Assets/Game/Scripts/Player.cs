using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speedPlayer;
    [SerializeField] private Transform _pointerClick;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody _rigidbody;
    private Ray _ray;
    private RaycastHit _hit;
    private bool _click;
    private Camera _camera;
    private Vector3 _vectorForce;
    private void Awake()
    {
        if (_speedPlayer > 30) throw new System.Exception("Задана слишком высокая скорость");
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _click = true;
            GetRayByClick();
        }
        if (_click) 
        { 
            Move();
            StopPlayer();
        }
        
    }

    private void GetRayByClick()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, layerMask))
        {
            _rigidbody.isKinematic = false;
            _pointerClick.position = _hit.point;
            _vectorForce = ((_hit.point - transform.position).normalized);
        }
    }

    private void Move()
    {
        _rigidbody.velocity = _vectorForce * _speedPlayer;
    }

    private void StopPlayer()
    {
        if ((int)_pointerClick.position.x == (int)transform.position.x && (int)_pointerClick.position.z == (int)transform.position.z)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
