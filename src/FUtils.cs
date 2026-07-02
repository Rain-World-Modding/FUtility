using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FUtility.Internal;
using UnityEngine;

namespace FUtility
{
    /// <summary>
    /// General use functions
    /// </summary>
    public static class FUtils
    {
        /// <summary>
        /// <para>
        /// Loads embedded sprites and atlases from an assembly.
        /// The sprites are loaded with names based on their files.
        /// All images found ending with .png are considered.
        /// They are loaded individually if no .txt file with the same name is found, 
        /// and as an atlas if a .txt file with the same name is found.
        /// Atlases are loaded in the same format as Futile loads them.
        /// All images are also loaded with <see cref="FilterMode.Point"/> filtering.
        /// </para>
        /// <para>
        /// To get the current executing assembly, use <see cref="Assembly.GetExecutingAssembly"/>.
        /// To embed resources within an assembly, use the <c>EmbeddedResource</c> csproj property.
        /// </para>
        /// </summary>
        /// <param name="assembly">Assembly to load from</param>
        public static void LoadElementsFromAssembly(Assembly assembly)
        {
            var resources = assembly.GetManifestResourceNames();
            foreach (var resource in resources)
            {
                var ending = Path.GetExtension(resource);
                if (ending == ".png")
                {
                    var noExtension = Path.GetFileNameWithoutExtension(resource);
                    string name = noExtension[(noExtension.LastIndexOf('.') + 1)..];

                    var tex = FUAtlasInternal.LoadTextureFromStream(assembly.GetManifestResourceStream(resource));
                    var atlas = Futile.atlasManager.LoadAtlasFromTexture(name, tex, false);

                    if (resources.Contains(noExtension + ".txt", StringComparer.OrdinalIgnoreCase))
                    {
                        var atlasData = new StreamReader(assembly.GetManifestResourceStream(noExtension + ".txt")!).ReadToEnd();
                        FUAtlasInternal.LoadDataIntoAtlas(atlas, atlasData);
                    }
                }
            }
        }

        /// <summary>
        /// Logs all loaded Futile elements in alphabetical order
        /// </summary>
        public static void LogAllElements()
        {
            foreach (var el in Futile.atlasManager._allElementsByName.Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
            {
                Debug.Log(el);
            }
        }

    }
}
