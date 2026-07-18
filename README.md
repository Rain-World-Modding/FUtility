# FUtility
Futile utility library that adds a bunch of nice utilities for advanced modders who have realized the futility of using vanilla Futile and desire more.

## What does this actually do?
Pretty much nothing. The code only contains stuff for other mods to use if they so choose. It does nothing on its own, and is completely useless for almost all Rain World modders. The target audience is mainly those who are working with complex shaders that require more per-vertex inputs than Futile usually allows.

As for specifics, it has a modified version of FSprite and TriangleMesh that allow accessing all 8 UV channels, as well as the normals channel and tangents channel. These do nothing on their own and are only useful for shaders that might require them. It is a Futile-based alternative to creating and managing a Unity GameObject from the ground up.

There are also some minor helper functions located in the `FUtility.FUtils` static class and an updated version of `FShader` found in the `FUtility.Shaders` namespace.

## What is Futile?
If you somehow live under a rock or haven't heard of the horrors of Rain World's code base, Futile is the very heavily code-based sprite framework which Rain World uses a modified version of. It was developed by Matt Rix and is hosted under a MIT license on GitHub ([link to Futile's source code](https://github.com/MattRix/Futile/)). Unfortunately for modders, the version of Futile that Rain World uses dates back to around 2014, and as such is missing some of the "luxuries" of Futile's modern codebase.

## I'm not a coder. How do I download this?
If you have access to the Steam Workshop, you can install the mod from [its Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=3756041520). Otherwise, you can download from the [Releases tab](https://github.com/Rain-World-Modding/FUtility/releases). Download the .zip file with `futility` in the name. **Do not download any other files!**

## I'm a coder. How do I use this?
### Step 1: Reference
Download FUtility.dll and FUtility.xml from the latest version in the [Releases tab](https://github.com/Rain-World-Modding/FUtility/releases) and put it in your references folder so you can reference it in your C# project. Then, do your graphics code work as you would normally, but with the alternatives in the `FUtility.Sprites` namespace.

Also make sure to add the mod itself as a dependency to your code mod, both in your modinfo.json and in your code as a `BepInDependency` attribute attached to your Plugin class. If you don't know how to do that, just plop `[BepInDependency("futility", BepInDependency.DependencyFlags.HardDependency)]` next to your `[BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]` line.

### Step 2: Shader modifications
In order to take advantage of the channels that FUtility provides, you will need to change what structs are used in your shader.

You should define the following struct:

```hlsl
struct appdata_futility {
    float4 vertex    : POSITION;
    float4 tangent   : TANGENT;
    float3 normal    : NORMAL;
    float4 color     : COLOR;
    float2 texcoord  : TEXCOORD0;
    float2 texcoord1 : TEXCOORD1;
    float2 texcoord2 : TEXCOORD2;
    float2 texcoord3 : TEXCOORD3;
    float2 texcoord4 : TEXCOORD4;
    float2 texcoord5 : TEXCOORD5;
    float2 texcoord6 : TEXCOORD6;
    float2 texcoord7 : TEXCOORD7;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};
```

This will let you access all properties that Unity sets, including ones that it doesn't normally allow access to by default with the `appdata_full` struct. Use the `appdata_futility` struct as the parameter to your vertex shader.

It should be fairly obvious what properties map to what, but in case you need some explanation, assume that you have defined your vertex shader as `v2f vert (appdata_futility v)`:
- The colors channel maps to `v.color`
- UV channel 1 maps to `v.texcoord`
- UV channel 2 maps to `v.texcoord1`
- UV channel 3 maps to `v.texcoord2`
- UV channel 4 maps to `v.texcoord3`
- UV channel 5 maps to `v.texcoord4`
- UV channel 6 maps to `v.texcoord5`
- UV channel 7 maps to `v.texcoord6`
- UV channel 8 maps to `v.texcoord7`
- The normals channel maps to `v.normal`
- The tangents channel maps to `v.tangent`

In total, this is 27 total floats of data that you can utilize in your shader, excluding the vertex position, which you probably shouldn't alter anyway.

### Step 3: Suffer
Have fun with your new control. If you release your mod on Steam, make sure to [add the mod as a dependency](https://steamcommunity.com/sharedfiles/filedetails/?id=3756041520)!
