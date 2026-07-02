using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace FUtility.Internal
{
    /// <summary>
    /// Internal helper methods for atlas loading
    /// </summary>
    internal static class FUAtlasInternal
    {
        public static Texture2D LoadTextureFromStream(Stream stream, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Clamp)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            var tex = new Texture2D(0, 0, TextureFormat.ARGB32, false)
            {
                filterMode = filterMode,
                wrapMode = wrapMode
            };
            tex.LoadImage(bytes);
            return tex;
        }

        public static void LoadDataIntoAtlas(FAtlas atlas, string rawJson)
        {
            Dictionary<string, object> dictionary = rawJson.dictionaryFromJson();
            if (dictionary == null)
            {
                Debug.LogWarning("Invalid JSON for atlas! Tried to load:");
                Debug.LogWarning(rawJson);
                throw new FutileException("The atlas loaded through LoadDataIntoAtlas was not a proper JSON file. Make sure to select \"Unity3D\" in TexturePacker.");
            }

            foreach (string el in atlas._elementsByName.Keys)
            {
                Futile.atlasManager._allElementsByName.Remove(el);
            }
            atlas._elements.Clear();
            atlas._elementsByName.Clear();
            atlas._isSingleImage = false;

            Dictionary<string, object> frames = (Dictionary<string, object>)dictionary["frames"];
            float resourceScaleInverse = Futile.resourceScaleInverse;
            int indexInAtlas = 0;
            foreach (KeyValuePair<string, object> keyValuePair in frames)
            {
                var element = new FAtlasElement
                {
                    indexInAtlas = indexInAtlas++
                };

                string name = keyValuePair.Key;
                if (Futile.shouldRemoveAtlasElementFileExtensions)
                {
                    int dotIndex = name.LastIndexOf(".");
                    if (dotIndex >= 0)
                    {
                        name = name[..dotIndex];
                    }
                }
                element.name = name;

                IDictionary itemDict = (IDictionary)keyValuePair.Value;

                element.isTrimmed = (bool)itemDict["trimmed"];

                IDictionary frame = (IDictionary)itemDict["frame"];
                float fx = float.Parse(frame["x"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                float fy = float.Parse(frame["y"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                float fw = float.Parse(frame["w"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                float fh = float.Parse(frame["h"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                var uvRect = new Rect(fx / atlas._textureSize.x, (atlas._textureSize.y - fy - fh) / atlas._textureSize.y, fw / atlas._textureSize.x, fh / atlas._textureSize.y);
                element.uvRect = uvRect;
                element.uvTopLeft.Set(uvRect.xMin, uvRect.yMax);
                element.uvTopRight.Set(uvRect.xMax, uvRect.yMax);
                element.uvBottomRight.Set(uvRect.xMax, uvRect.yMin);
                element.uvBottomLeft.Set(uvRect.xMin, uvRect.yMin);

                // Untrimmed size
                IDictionary sourcePixelSize = (IDictionary)itemDict["sourceSize"];
                element.sourcePixelSize.x = float.Parse(sourcePixelSize["w"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                element.sourcePixelSize.y = float.Parse(sourcePixelSize["h"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
                element.sourceSize.x = element.sourcePixelSize.x * resourceScaleInverse;
                element.sourceSize.y = element.sourcePixelSize.y * resourceScaleInverse;

                // Trimmed size
                IDictionary sourceRect = (IDictionary)itemDict["spriteSourceSize"];
                float x = float.Parse(sourceRect["x"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture) * resourceScaleInverse;
                float y = float.Parse(sourceRect["y"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture) * resourceScaleInverse;
                float w = float.Parse(sourceRect["w"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture) * resourceScaleInverse;
                float h = float.Parse(sourceRect["h"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture) * resourceScaleInverse;
                element.sourceRect = new Rect(x, y, w, h);
                atlas._elements.Add(element);
                atlas._elementsByName.Add(element.name, element);
                element.atlas = atlas;
                element.atlasIndex = atlas.index;
                Futile.atlasManager._allElementsByName.Add(element.name, element);
            }
        }
    }
}
