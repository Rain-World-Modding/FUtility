using System;
using FUtility.Internal;
using UnityEngine;

namespace FUtility.Facets
{
    /// <summary>
    /// Derivative of <see cref="FQuadRenderLayer"/> with more channels
    /// </summary>
    public class FUQuadRenderLayer : FQuadRenderLayer, IFURenderLayer
    {
        /// <summary>Privately stored array of UV channel 2</summary>
        protected Vector2[] _uvs2 = [];
        /// <summary>Privately stored array of UV channel 3</summary>
        protected Vector2[] _uvs3 = [];
        /// <summary>Privately stored array of UV channel 4</summary>
        protected Vector2[] _uvs4 = [];
        /// <summary>Privately stored array of UV channel 5</summary>
        protected Vector2[] _uvs5 = [];
        /// <summary>Privately stored array of UV channel 6</summary>
        protected Vector2[] _uvs6 = [];
        /// <summary>Privately stored array of UV channel 7</summary>
        protected Vector2[] _uvs7 = [];
        /// <summary>Privately stored array of UV channel 8</summary>
        protected Vector2[] _uvs8 = [];

        /// <summary>Privately stored array of vertex normals</summary>
        protected Vector3[] _normals = [];

        /// <summary>Privately stored array of vertex tangents</summary>
        protected Vector4[] _tangents = [];

        /// <inheritdoc/>
        public Vector2[] uvs2 => _uvs2;
        /// <inheritdoc/>
        public Vector2[] uvs3 => _uvs3;
        /// <inheritdoc/>
        public Vector2[] uvs4 => _uvs4;
        /// <inheritdoc/>
        public Vector2[] uvs5 => _uvs5;
        /// <inheritdoc/>
        public Vector2[] uvs6 => _uvs6;
        /// <inheritdoc/>
        public Vector2[] uvs7 => _uvs7;
        /// <inheritdoc/>
        public Vector2[] uvs8 => _uvs8;

        /// <inheritdoc/>
        public Vector3[] normals => _normals;

        /// <inheritdoc/>
        public Vector4[] tangents => _tangents;

        /// <summary>
        /// Derivative of <see cref="FQuadRenderLayer"/> with more channels
        /// </summary>
        /// <param name="stage">The Futile stage</param>
        /// <param name="facetType">The facet type (should be <see cref="FUFacetTypes.FUQuadType"/>)</param>
        /// <param name="atlas">The FAtlas to use</param>
        /// <param name="shader">The shader to use</param>
        protected internal FUQuadRenderLayer(FStage stage, FFacetType facetType, FAtlas atlas, FShader shader) : base(stage, facetType, atlas, shader)
        {
        }

        /// <inheritdoc/>
        public override void ShrinkMaxFacetLimit(int deltaDecrease)
        {
            base.ShrinkMaxFacetLimit(deltaDecrease);

            if (deltaDecrease <= 0) return;
            Array.Resize(ref _uvs2, _maxFacetCount);
            Array.Resize(ref _uvs3, _maxFacetCount);
            Array.Resize(ref _uvs4, _maxFacetCount);
            Array.Resize(ref _uvs5, _maxFacetCount);
            Array.Resize(ref _uvs6, _maxFacetCount);
            Array.Resize(ref _uvs7, _maxFacetCount);
            Array.Resize(ref _uvs8, _maxFacetCount);
            Array.Resize(ref _normals, _maxFacetCount);
            Array.Resize(ref _tangents, _maxFacetCount);
        }

        /// <inheritdoc/>
        public override void ExpandMaxFacetLimit(int deltaIncrease)
        {
            base.ExpandMaxFacetLimit(deltaIncrease);

            if (deltaIncrease <= 0) return;
            Array.Resize(ref _uvs2, _maxFacetCount);
            Array.Resize(ref _uvs3, _maxFacetCount);
            Array.Resize(ref _uvs4, _maxFacetCount);
            Array.Resize(ref _uvs5, _maxFacetCount);
            Array.Resize(ref _uvs6, _maxFacetCount);
            Array.Resize(ref _uvs7, _maxFacetCount);
            Array.Resize(ref _uvs8, _maxFacetCount);
            Array.Resize(ref _normals, _maxFacetCount);
            Array.Resize(ref _tangents, _maxFacetCount);
        }
    }
}
