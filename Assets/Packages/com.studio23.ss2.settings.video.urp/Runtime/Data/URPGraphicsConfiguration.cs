
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
        private UniversalRenderPipelineAsset _pipelineAsset;
        private Bloom _bloom;
        private ColorAdjustments _colorAdjustments;
        private readonly string _ambientOcclusionRfString = "SSAO";
        private ScriptableRendererFeature _ambientOcclusion;


        public override void Initialize(Volume currentVolume)
        {
            _pipelineAsset = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset);
            CurrentVolumeProfile = currentVolume.profile;
            CurrentVolumeProfile.TryGet(typeof(Bloom), out _bloom);
            CurrentVolumeProfile.TryGet(typeof(ColorAdjustments), out _colorAdjustments);
            _ambientOcclusion = GetAmbientOcclusion();
        }

        public override void SetBloomState(bool state)
        {
            if(_bloom == null) return;
            _bloom.active = state;
        }

        public override void SetAmbientOcclusionState(bool state)
        {
            if (_ambientOcclusion == null) return;
            _ambientOcclusion.SetActive(state);
        }

        public override void SetBrightness(float brightnessValue)
        {
            if (_colorAdjustments == null) return;
            _colorAdjustments.postExposure.value = brightnessValue;
        }

        public override void SetRenderScale(float scaleValue)
        {
            if(_pipelineAsset == null) return;
            _pipelineAsset.renderScale = scaleValue;
        }

        public override void UpdatePipelineRenderAsset()
        {
            float renderScaleValue = _pipelineAsset.renderScale;
            bool isAmbientOcclusion = _ambientOcclusion.isActive;

            _pipelineAsset = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset);
            UpdateAmbientOcclusion(isAmbientOcclusion);
            SetRenderScale(renderScaleValue);
        }

        private void UpdateAmbientOcclusion(bool state)
        {
            _ambientOcclusion = GetAmbientOcclusion();
            SetAmbientOcclusionState(state);
        }

        private ScriptableRendererFeature GetAmbientOcclusion()
        {
            var renderer = _pipelineAsset.GetRenderer(0);
            var property = typeof(ScriptableRenderer).GetProperty("rendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance);
            if (property == null)
            {
                Debug.Log("No property found in renderer");
                return null;
            }
            List<ScriptableRendererFeature> rendererFeatures = property.GetValue(renderer) as List<ScriptableRendererFeature>;
            if (rendererFeatures == null)
            {
                Debug.Log("No Scriptable Renderer Feature found");
                return null;
            }

            foreach (var rf in rendererFeatures)
            {
                if (rf.name.Equals(_ambientOcclusionRfString))
                {
                    _ambientOcclusion = rf;
                    return _ambientOcclusion;

                }
            }
            return null;
        }

    }
}
