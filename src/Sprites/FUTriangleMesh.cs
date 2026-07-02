using System;
using FUtility.Facets;
using UnityEngine;

namespace FUtility.Sprites
{
    /// <summary>
    /// Derivative of <see cref="TriangleMesh"/> with extra channels
    /// </summary>
    public class FUTriangleMesh : TriangleMesh, IFUSprite
    {
        /// <inheritdoc/>
        public virtual bool IsUVChannel1Restricted => false;

        /// <summary>Privately stored array of UV channel 2</summary>
        protected Vector2[] _uvs2;
        /// <summary>Privately stored array of UV channel 3</summary>
        protected Vector2[] _uvs3;
        /// <summary>Privately stored array of UV channel 4</summary>
        protected Vector2[] _uvs4;
        /// <summary>Privately stored array of UV channel 5</summary>
        protected Vector2[] _uvs5;
        /// <summary>Privately stored array of UV channel 6</summary>
        protected Vector2[] _uvs6;
        /// <summary>Privately stored array of UV channel 7</summary>
        protected Vector2[] _uvs7;
        /// <summary>Privately stored array of UV channel 8</summary>
        protected Vector2[] _uvs8;

        /// <summary>Privately stored array of vertex normals</summary>
        protected Vector3[] _normals;
        /// <summary>Privately stored array of vertex tangents</summary>
        protected Vector4[] _tangents;

        /// <inheritdoc/>
        public Vector2[] uvs1 => UVvertices;

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

        /// <inheritdoc/>
        public Color[] colors => verticeColors;

        /// <summary>Quick cast for <see cref="IFURenderLayer"/></summary>
        protected IFURenderLayer _fuRenderLayer => (_renderLayer as IFURenderLayer)!;

        /// <summary>
        /// Derivative of <see cref="TriangleMesh"/> with extra channels
        /// </summary>
        /// <param name="imageName">Atlas element name to use</param>
        /// <param name="tris">Triangles</param>
        /// <param name="atlasedImage">Whether to initialize UV channel 1 to the center of the UV rect of the atlas element</param>
        public FUTriangleMesh(string imageName, Triangle[] tris, bool atlasedImage = false) : base(imageName, tris, true, atlasedImage)
        {
            _uvs2 = new Vector2[vertices.Length];
            _uvs3 = new Vector2[vertices.Length];
            _uvs4 = new Vector2[vertices.Length];
            _uvs5 = new Vector2[vertices.Length];
            _uvs6 = new Vector2[vertices.Length];
            _uvs7 = new Vector2[vertices.Length];
            _uvs8 = new Vector2[vertices.Length];
            _normals = new Vector3[vertices.Length];
            _tangents = new Vector4[vertices.Length];
            Init(FUFacetTypes.FUTriangleType, _atlas, _numberOfFacetsNeeded);
        }

        /// <inheritdoc/>
        public void MarkAsDirty()
        {
            _isMeshDirty = true;
            _isAlphaDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetColor(Color color, int index)
        {
            verticeColors[index] = color;
            _isMeshDirty = true;
            _isAlphaDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetColors(Color color)
        {
            for (int i = 0; i < verticeColors.Length; i++)
            {
                verticeColors[i] = color;
            }

            _isMeshDirty = true;
            _isAlphaDirty = true;

        }

        /// <inheritdoc/>
        public virtual void SetNormal(Vector3 normal, int index)
        {
            _normals[index] = normal;
            _isMeshDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetNormals(Vector3 normal)
        {
            for (int i = 0; i < _normals.Length; i++)
            {
                _normals[i] = normal;
            }

            _isMeshDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetTangent(Vector4 tangent, int index)
        {
            _tangents[index] = tangent;
            _isMeshDirty = true;
        }

        /// <inheritdoc/>
        public virtual void SetTangents(Vector4 tangent)
        {
            for (int i = 0; i < _tangents.Length; i++)
            {
                _tangents[i] = tangent;
            }

            _isMeshDirty = true;
        }

        /// <summary>
        /// Sets a UV of a channel
        /// </summary>
        /// <param name="uv">Value to set</param>
        /// <param name="index">Index of array to set</param>
        /// <param name="channel">Number between 1 and 8</param>
        public virtual void SetUV(int index, Vector2 uv, int channel)
        {
            Vector2[] array = channel switch
            {
                1 => UVvertices,
                2 => _uvs2,
                3 => _uvs3,
                4 => _uvs4,
                5 => _uvs5,
                6 => _uvs6,
                7 => _uvs7,
                8 => _uvs8,
                _ => throw new IndexOutOfRangeException($"{nameof(channel)} was not in the specified range!")
            };

            array[index] = uv;

            _isMeshDirty = true;
        }

        /// <summary>
        /// Sets all the UVs of a channel to a single value
        /// </summary>
        /// <param name="uv">Value to set</param>
        /// <param name="channel">Number between 1 and 8</param>
        public virtual void SetUVs(Vector2 uv, int channel)
        {
            Vector2[] array = channel switch
            {
                1 => UVvertices,
                2 => _uvs2,
                3 => _uvs3,
                4 => _uvs4,
                5 => _uvs5,
                6 => _uvs6,
                7 => _uvs7,
                8 => _uvs8,
                _ => throw new IndexOutOfRangeException($"{nameof(channel)} was not in the specified range!")
            };

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = uv;
            }

            _isMeshDirty = true;
        }


        /// <inheritdoc/>
        public override void PopulateRenderLayer()
        {
            base.PopulateRenderLayer();
            if (_isOnStage && _firstFacetIndex != -1)
            {
                Vector2[] uvs2 = _fuRenderLayer.uvs2;
                Vector2[] uvs3 = _fuRenderLayer.uvs3;
                Vector2[] uvs4 = _fuRenderLayer.uvs4;
                Vector2[] uvs5 = _fuRenderLayer.uvs5;
                Vector2[] uvs6 = _fuRenderLayer.uvs6;
                Vector2[] uvs7 = _fuRenderLayer.uvs7;
                Vector2[] uvs8 = _fuRenderLayer.uvs8;
                Vector3[] normals = _fuRenderLayer.normals;
                Vector4[] tangents = _fuRenderLayer.tangents;

                int firstIndex = _firstFacetIndex * 4;
                for (int i = 0; i < 4; i++)
                {
                    uvs2[firstIndex + i] = _uvs2[i];
                    uvs3[firstIndex + i] = _uvs3[i];
                    uvs4[firstIndex + i] = _uvs4[i];
                    uvs5[firstIndex + i] = _uvs5[i];
                    uvs6[firstIndex + i] = _uvs6[i];
                    uvs7[firstIndex + i] = _uvs7[i];
                    uvs8[firstIndex + i] = _uvs8[i];
                    normals[firstIndex + i] = _normals[i];
                    tangents[firstIndex + i] = _tangents[i];
                }
                _renderLayer.HandleVertsChange();
            }
        }
    }
}
