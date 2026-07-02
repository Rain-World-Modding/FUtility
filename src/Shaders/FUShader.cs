using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUtility.Internal;
using UnityEngine;

namespace FUtility.Shaders
{
    /// <summary>
    /// Backport and modified version of the modern version of FShader
    /// </summary>
    public class FUShader : FShader
    {
        static FUShader()
        {
            FUShaderInternal.Apply();
        }

        /// <summary>
        /// Whether to use the <see cref="Apply(Material)"/> function
        /// </summary>
        public bool needsApply = false;

        /// <summary>
        /// Alternative version of FShader with more features.
        /// </summary>
        /// <param name="name">Name of the shader</param>
        /// <param name="shader">Shader to use</param>
        public FUShader(string name, Shader shader) : base(name, shader, FShader._nextShaderIndex++)
        {
            FShader._shaders.Add(this);
        }

        /// <summary>
        /// Alternative version of FShader with more features.
        /// </summary>
        /// <param name="name">Name of the shader</param>
        /// <param name="shader">Shader to use</param>
        /// <param name="keywords">Keywords to apply</param>
        public FUShader(string name, Shader shader, string[] keywords) : this(name, shader)
        {
            this.keywords = keywords;
        }

        /// <summary>
        /// To be overridden. Applies material properties to the given material.
        /// </summary>
        /// <param name="material">Material to apply to</param>
        public virtual void Apply(Material material)
        {
            // for implementor to fill out
        }

        /// <summary>
        /// Initializes a shader with a short name and a shader reference
        /// </summary>
        /// <param name="shaderShortName">Short name to use</param>
        /// <param name="shader">Shader instance</param>
        /// <returns>A new <see cref="FUShader"/> if any <see cref="FShader"/> with the same name was not found</returns>
        public static FShader FUCreateShader(string shaderShortName, Shader shader)
        {
            for (int i = 0; i < FShader._shaders.Count; i++)
            {
                if (FShader._shaders[i].name == shaderShortName)
                {
                    return FShader._shaders[i];
                }
            }
            var fshader = new FShader(shaderShortName, shader, FShader._nextShaderIndex++);
            FShader._shaders.Add(fshader);
            return fshader;
        }

        /// <summary>
        /// Initializes a shader with a short name, a shader reference, and shader keywords
        /// </summary>
        /// <param name="shaderShortName">Short name to use</param>
        /// <param name="shader">Shader instance</param>
        /// <param name="keywords">Keywords to apply to the shader</param>
        /// <returns>A new <see cref="FUShader"/> if any <see cref="FShader"/> with the same name was not found</returns>
        public static FShader FUCreateShader(string shaderShortName, Shader shader, string[] keywords)
        {
            var fshader = FUCreateShader(shaderShortName, shader);
            fshader.keywords = keywords;
            return fshader;
        }
    }
}
