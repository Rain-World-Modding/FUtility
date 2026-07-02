using FUtility.Shaders;

namespace FUtility.Internal
{
    internal static class FUShaderInternal
    {
        private static bool _hasApplied = false;
        internal static void Apply()
        {
            if (_hasApplied) return;
            _hasApplied = true;
            On.FFacetRenderLayer.Update += FFacetRenderLayer_Update;
        }

        private static void FFacetRenderLayer_Update(On.FFacetRenderLayer.orig_Update orig, FFacetRenderLayer self, int depth)
        {
            orig(self, depth);
            if (self._shader is FUShader fuShader && fuShader.needsApply)
            {
                fuShader.Apply(self._material);
            }
        }
    }
}
