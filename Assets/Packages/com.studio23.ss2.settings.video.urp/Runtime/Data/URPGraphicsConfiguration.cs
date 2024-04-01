
using Studio23.SS2.Settings.Video.Data;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace Studio23.SS2.Settings.Video.URP.Data
{
    public class URPGraphicsConfiguration : GraphicsConfigurationBase
    {
        private Bloom _bloom;
        private ColorAdjustments _colorAdjustments;

        private readonly string _ambientOcclusionRfString = "SSAO";
        private ScriptableRendererFeature _ambientOcclusion;
        public bool IsSSAOActiveAtFirst;


        public override void Initialize(Volume currentVolume)
        {
            CurrentVolumeProfile = currentVolume.profile;
            CurrentVolumeProfile.TryGet(typeof(Bloom), out _bloom);
            CurrentVolumeProfile.TryGet(typeof(ColorAdjustments), out _colorAdjustments);
            UpdateAmbientOcclusionRenderFeature();
        }

        public override void SetBloomState(bool state)
        {
            if(!_bloom) return;
            _bloom.active = state;
        }

        public override void SetAmbientOcclusionState(bool state)
        {
            if (!_ambientOcclusion) return;
            _ambientOcclusion.SetActive(state);
        }

        public override void SetBrightness(float brightnessValue)
        {
            if (!_colorAdjustments) return;
            _colorAdjustments.postExposure.value = brightnessValue;
        }

        public override void UpdateAmbientOcclusionRenderFeature()
        {
            var renderAsset = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset);
            if (renderAsset == null)
            {
                Debug.Log("No render asset found");
                return;
            }
            var renderer = renderAsset.GetRenderer(0);
            var property = typeof(ScriptableRenderer).GetProperty("rendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance);
            if (property == null)
            {
                Debug.Log("No property found in renderer");
                return;
            }
            List<ScriptableRendererFeature> rendererFeatures = property.GetValue(renderer) as List<ScriptableRendererFeature>;
            if (rendererFeatures == null)
            {
                Debug.Log("No Scriptable Renderer Feature found");
                return;
            }

            foreach (var rf in rendererFeatures)
            {
                if (rf.name.Equals(_ambientOcclusionRfString))
                {
                    bool isEnabled = IsSSAOActiveAtFirst;
                    if (_ambientOcclusion != null)
                        isEnabled = _ambientOcclusion.isActive;
                    _ambientOcclusion = rf;
                    SetAmbientOcclusionState(isEnabled);
                    break;
                }
            }
        }
    }
}
