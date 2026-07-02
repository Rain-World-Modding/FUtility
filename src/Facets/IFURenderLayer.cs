using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FUtility.Facets
{
    /// <summary>
    /// Interface for FUtility custom render layers
    /// </summary>
    public interface IFURenderLayer
    {
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
    }
}
