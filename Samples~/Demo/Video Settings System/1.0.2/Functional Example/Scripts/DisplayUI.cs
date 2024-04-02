
using System.Collections.Generic;
using Studio23.SS2.Settings.Video.Core;
using UnityEngine;

namespace Studio23.SS2.Settings.Video.Samples
{
    public class DisplayUI : MonoBehaviour
    {
        [SerializeField] private LoopedStepper _resolutionstepper;
        [SerializeField] private LoopedStepper _displayModeStepper;
        [SerializeField] private LoopedStepper _vSyncStepper;

        private DisplayController _displayController;

        void Start()
        {
            _displayController = VideoSettingsManager.Instance.DisplayController;
            InitializeUI();
        }

        private void InitializeUI()
        {
            _resolutionstepper.InitializeData(_displayController.GetSupportedResolutions(), _displayController.GetSelectedResolutionIndex());
            _resolutionstepper.SelectedIndexUpdated.AddListener(_displayController.ChangeResolution);
            
            _displayModeStepper.InitializeData(_displayController.GetFullScreenModes());
            _displayModeStepper.SelectedIndexUpdated.AddListener(_displayController.ChangeFullScreenMode);
            
            _vSyncStepper.InitializeData(new List<string>() { "Off", "On", "Every 2nd V-Blank" });
            _vSyncStepper.SelectedIndexUpdated.AddListener(_displayController.ChangeVSync);
        }
    }
}

