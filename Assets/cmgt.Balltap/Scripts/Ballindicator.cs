using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cmgt.Balltap
{
    public class Ballindicator : MonoBehaviour
    {
        public Transform _targetPosition;
        public Vector3 _offset;
        public Transform _ui;
        Camera _camera;
        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
            _ui.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(_targetPosition.transform.position);
            Vector3 worldToScreen = _camera.WorldToScreenPoint(_targetPosition.position + _offset);
            Debug.Log(worldToScreen);
            worldToScreen.z = 0;
            worldToScreen.y = 0;
            float dot = Vector3.Dot(_camera.transform.forward, (_targetPosition.position - _camera.transform.position).normalized);
            _ui.gameObject.SetActive(dot > 0f);
            _ui.position = worldToScreen;

            //PlaneRewardController _c = FindObjectOfType<PlaneRewardController>();
            //if (_c.tracker_show == false)
            //{
            //    _ui.gameObject.SetActive(false);
            //}
        }
    }
}
