using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utility;

public class PlayerCtrl : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private PhotonView pv;

    private Transform camPivot;

    private float v;
    private float h;
    private float r_x;
    private float r_y;

    [Header("이동 및 회전 속도")]
    public float moveSpeed = 8.0f;
    public float turnSpeed = 0.0f;
    public float jumpPower = 5.0f;

    private float turnSpeedValue = 200.0f;

    RaycastHit hit;

    IEnumerator Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.5f);

        if (pv.IsMine)
        {
            camPivot = transform.Find("CamPivot").transform;

            Camera.main.transform.parent = camPivot;
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.rotation = camPivot.rotation;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        turnSpeed = turnSpeedValue;
    }

    // 키 입력
    void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        r_x = Input.GetAxis("Mouse X");
        r_y = Input.GetAxis("Mouse Y");

        Debug.DrawRay(transform.position, -transform.up * 0.6f, Color.green);
        if (Input.GetKeyDown("space"))
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, 0.6f))
            {
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    // 물리적 처리
    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
            transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.Self);
            transform.Rotate(Vector3.up * Time.smoothDeltaTime * turnSpeed * r_x);

            if (camPivot.rotation.x >= -0.5f && camPivot.rotation.x <= 0.8f)
            {
                Debug.Log($"camPivot.rotation.x : {camPivot.rotation.x}");
                Debug.Log($"camPivot.rotation.x : {camPivot.localRotation.x}");
                camPivot.Rotate(Vector3.right * Time.smoothDeltaTime * turnSpeed * r_y);
            }
        }
    }

}