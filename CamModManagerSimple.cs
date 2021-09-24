using BepInEx;
using UnityEngine;
using Amplitude.Mercury.Presentation;

namespace HkFPCameraModSimple
{
    [BepInPlugin("humankind.rykolo.HkFPCameraModSimple", "Humankind FPCamera Mod Simple", "1.0.0.1")]
    public class CamModManagerSimple : BaseUnityPlugin
    {
        public bool cameraBool;
        private float nearClip = 33.93487f; //orig values
        private float farClip = 113.942f;

        private GameObject cameraGo;
        private PresentationCameraMover presCamMover;
        private FPcameraSimple FPcam;
        private Camera cameraCam;

        private void CameraSetup()
        {
            cameraBool = false;
            cameraGo = GameObject.Find("Camera");
            if (cameraGo == null)
                { Logger.LogError("NullRef, No Camera GameObject found"); }

            //this is the default script the game uses to control the camera
            presCamMover = cameraGo.GetComponent<PresentationCameraMover>();
            if (presCamMover == null)
                { Logger.LogError("NullRef, No PresentationCameraMover found"); }

            FPcam = cameraGo.AddComponent<FPcameraSimple>();
            if (FPcam == null) 
                { Logger.LogError("NullRef, FPcamera script did not load"); }

            cameraCam = cameraGo.GetComponent<Camera>();
            if (cameraCam == null)
                { Logger.LogError("NullRef, No camera \"Camera\" component found"); }

            nearClip = cameraCam.nearClipPlane;   //in case of different camera settings
            farClip = cameraCam.farClipPlane;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (!cameraGo)
                {
                    CameraSetup();
                }

                cameraBool = !cameraBool;
                presCamMover.enabled = !cameraBool;
                FPcam.enabled = cameraBool;
                //GUI.enabled = !GUI.enabled;

                if (cameraBool == true) 
                { 
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    cameraCam.nearClipPlane = 0.01f;
                    cameraCam.farClipPlane = farClip * 2;
                }
                else 
                { 
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    cameraCam.nearClipPlane = nearClip;
                    cameraCam.farClipPlane = farClip;
                }
            }

            //ALL CAMERAS IN SCENE:
            //ImpostorCamera
            //AvatarCamera
            //Camera        <---
            //MainView
            //UIFxCamera



            //ALL RENDER LAYERS IN SCENE:
            //Default
            //TransparentFX
            //Ignore Raycast

            //Water
            //UI

            //Overlay
            //PostProcess
            //PresentationCursorTarget
            //AvatarVisible
            //AvatarHidden
            //PresentationPawn
            //PresentationPawnHidden
            //PresentationPawnGhost
            //PresentationPawnGhostAndOpaque

        }
    }
}
