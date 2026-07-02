using FUtility.Internal;

namespace FUtility.Facets
{
    /// <summary>
    /// Holds all registered facet types of FUtility
    /// </summary>
    public static class FUFacetTypes
    {
        /// <summary>Derivative of quad facet type with additional selectable channels.</summary>
        public static FFacetType FUQuadType { get; private set; }

        /// <summary>Derivative of triangle facet type with additional selectable channels.</summary>
        public static FFacetType FUTriangleType { get; private set; }

        static FUFacetTypes()
        {
            FUFacetInternal.Apply();
            FUQuadType = FFacetType.CreateFacetType("FUQuad", 10, 10, 60, CreateFUQuadRenderlayer);
            FUTriangleType = FFacetType.CreateFacetType("FUTriangle", 16, 16, 64, CreateFUTriangleRenderlayer);
        }

        private static FFacetRenderLayer CreateFUQuadRenderlayer(FStage stage, FFacetType facetType, FAtlas atlas, FShader shader)
        {
            return new FUQuadRenderLayer(stage, facetType, atlas, shader);
        }

        private static FFacetRenderLayer CreateFUTriangleRenderlayer(FStage stage, FFacetType facetType, FAtlas atlas, FShader shader)
        {
            return new FUTriangleRenderLayer(stage, facetType, atlas, shader);
        }
    }
}
