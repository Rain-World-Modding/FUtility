using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FUtility.Sprites
{
    /// <summary>
    /// Interface for all FUtility FSprite derivatives
    /// </summary>
    public interface IFUSprite
    {
        /// <summary>Whether channel 1 of UVs is a restricted channel.</summary>
        public bool IsUVChannel1Restricted { get; }

        /// <summary>Channel 1 of UVs. May be read-only depending on implementation.</summary>
        public Vector2[] uvs1 { get; }
        /// <summary>Channel 2 of UVs</summary>
        public Vector2[] uvs2 { get; }
        /// <summary>Channel 3 of UVs</summary>
        public Vector2[] uvs3 { get; }
        /// <summary>Channel 4 of UVs</summary>
        public Vector2[] uvs4 { get; }
        /// <summary>Channel 5 of UVs</summary>
        public Vector2[] uvs5 { get; }
        /// <summary>Channel 6 of UVs</summary>
        public Vector2[] uvs6 { get; }
        /// <summary>Channel 7 of UVs</summary>
        public Vector2[] uvs7 { get; }
        /// <summary>Channel 8 of UVs</summary>
        public Vector2[] uvs8 { get; }

        /// <summary>Vertex normals channel</summary>
        public Vector3[] normals { get; }

        /// <summary>Vertex tangents channel</summary>
        public Vector4[] tangents { get; }

        /// <summary>Vertex colors channel</summary>
        public Color[] colors { get; }

        /// <summary>
        /// Sets a UV of a channel
        /// </summary>
        /// <param name="uv">Value to set</param>
        /// <param name="index">Index of array to set</param>
        /// <param name="channel">Number between 1 and 8. Channel 1 may be reserved depending on implementation.</param>
        public void SetUV(int index, Vector2 uv, int channel);

        /// <summary>
        /// Sets all the UVs of a channel to a single value
        /// </summary>
        /// <param name="uv">Value to set</param>
        /// <param name="channel">Number between 1 and 8. Channel 1 may be reserved depending on implementation.</param>
        public void SetUVs(Vector2 uv, int channel);

        /// <summary>
        /// Sets a normal value.
        /// </summary>
        /// <param name="normal">Normal to set</param>
        /// <param name="index">Index to set at</param>
        public void SetNormal(Vector3 normal, int index);

        /// <summary>
        /// Sets all normals to the same value.
        /// </summary>
        /// <param name="normal">Value to set to</param>
        public void SetNormals(Vector3 normal);

        /// <summary>
        /// Sets a tangent value.
        /// </summary>
        /// <param name="tangent">Tangent to set</param>
        /// <param name="index">Index to set at</param>
        public void SetTangent(Vector4 tangent, int index);

        /// <summary>
        /// Sets all tangents to the same value
        /// </summary>
        /// <param name="tangent">Value to set to</param>
        public void SetTangents(Vector4 tangent);

        /// <summary>
        /// Sets a color value.
        /// </summary>
        /// <param name="color">Color to set</param>
        /// <param name="index">Index to set at</param>
        public void SetColor(Color color, int index);

        /// <summary>
        /// Sets all colors to the same value
        /// </summary>
        /// <param name="color">Value to set to</param>
        public void SetColors(Color color);

        /// <summary>
        /// Marks as dirty. Especially useful if using the array properties instead of the methods.
        /// </summary>
        public void MarkAsDirty();
    }
}
