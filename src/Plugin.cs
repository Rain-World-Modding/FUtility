using BepInEx;
using BepInEx.Logging;
using System.Security.Permissions;

// Allows access to private members
#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace FUtility;

[BepInPlugin("futility", "FUtility", "1.0")]
internal sealed class Plugin : BaseUnityPlugin { }
