
using System.Collections.Generic;
using Studio23.SS2.Settings.Video.Core;
using Studio23.SS2.Settings.Video.Samples;
using UnityEngine;

public class GrapicsUI : MonoBehaviour
{

    [SerializeField] private LoopedStepper _preset;
    [SerializeField] private LoopedStepper _shadowStepper;
    [SerializeField] private LoopedStepper _shadowResolution;
    [SerializeField] private LoopedStepper _textureQuality;
    [SerializeField] private LoopedStepper _bloom;
    [SerializeField] private LoopedStepper _ambientOcclusion;

    private GraphicsController _graphicsController;

    void Start()
    {
        _graphicsController = VideoSettingsManager.Instance.GraphicsController;
        InitializeUI();
    }

    private void InitializeUI()
    {
        _preset.InitializeData(new List<string>() { "High", "Medium", "Low", "Custom" });
        _preset.SelectedIndexUpdated.AddListener(_graphicsController.ChangeQualitySetting);

        _shadowStepper.InitializeData(_graphicsController.GetShadowType());
        _shadowStepper.SelectedIndexUpdated.AddListener(_graphicsController.ChangeShadowType);

        _shadowResolution.InitializeData(_graphicsController.GetShadowResolutions());
        _shadowResolution.SelectedIndexUpdated.AddListener(_graphicsController.ChangeShadowResolution);

        _textureQuality.InitializeData(new List<string>() { "Full", "Half", "Quarter", "Eighth" });
        _textureQuality.SelectedIndexUpdated.AddListener(_graphicsController.ChangeTextureQuality);

        _bloom.InitializeData(new List<string>() { "Off", "On" });
        _bloom.SelectedIndexUpdated.AddListener(_graphicsController.SetBloomState);

        _ambientOcclusion.InitializeData(new List<string>() { "Off", "On" });
        _ambientOcclusion.SelectedIndexUpdated.AddListener(_graphicsController.SetAmbientOcculsionState);

    }
}
