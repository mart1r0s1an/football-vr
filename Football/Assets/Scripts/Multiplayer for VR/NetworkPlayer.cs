/*
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    private PhotonView _photonView;

    private Transform _headRig;
    private Transform _leftHandRig;
    private Transform _rightHandRig;
    
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        XRRig rig = FindObjectOfType<XRRig>();
        
        //_headRig = rig.transform.Find("Camera Offset/Main Camera");
        _leftHandRig = rig.transform.Find("Camera Offset/Left Controller");
        _leftHandRig = rig.transform.Find("Camera Offset/Right Controller");
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
           // head.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
             
            //MapPosition(head, _headRig);
            MapPosition(leftHand, _leftHandRig);
            MapPosition(rightHand, _rightHandRig);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
*/
