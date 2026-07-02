using FUtility.Facets;

namespace FUtility.Internal
{
    internal static class FUFacetInternal
    {
        internal static void ApplyHooks()
        {
            On.FFacetRenderLayer.UpdateMeshProperties += FFacetRenderLayer_UpdateMeshProperties;
        }

        private static void FFacetRenderLayer_UpdateMeshProperties(On.FFacetRenderLayer.orig_UpdateMeshProperties orig, FFacetRenderLayer self)
        {
            bool didVertCountChange = self._didVertCountChange;
            bool didVertsChange = self._didVertsChange;
            //bool didColorsChange = self._didColorsChange;
            bool didUVsChange = self._didUVsChange;
            orig(self);
            if (self is IFURenderLayer fu)
            {
                if (didVertCountChange || didUVsChange)
                {
                    self._mesh.uv2 = fu.uvs2;
                    self._mesh.uv3 = fu.uvs3;
                    self._mesh.uv4 = fu.uvs4;
                    self._mesh.uv5 = fu.uvs5;
                    self._mesh.uv6 = fu.uvs6;
                    self._mesh.uv7 = fu.uvs7;
                    self._mesh.uv8 = fu.uvs8;
                }
                if (didVertCountChange || didVertsChange)
                {
                    self._mesh.normals = fu.normals;
                    self._mesh.tangents = fu.tangents;
                }
            }
        }
    }
}
