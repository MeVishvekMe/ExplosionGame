using System;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour {
    
    public CinemachineBrain cinemachineBrain;
    
    public CinemachineVirtualCamera idleCamera;
    public CinemachineVirtualCamera movingCamera;
    

    private void Update() {
        
        if (PlayerMechanics.isMoving) {
            cinemachineBrain.m_DefaultBlend.m_Time = 1f;
            movingCamera.Priority = 10;
            idleCamera.Priority = 9;
        }
        else {
            cinemachineBrain.m_DefaultBlend.m_Time = 2f;
            idleCamera.Priority = 10;
            movingCamera.Priority = 9;
        }
    }
}
