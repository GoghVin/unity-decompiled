﻿// Decompiled with JetBrains decompiler
// Type: UnityEngine.LODGroup
// Assembly: UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8FF7A2C-E4EE-4232-AB17-3FCABEC16496
// Assembly location: C:\Users\Blake\sandbox\unity\test-project\Library\UnityAssemblies\UnityEngine.dll

using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
  /// <summary>
  ///   <para>LODGroup lets you group multiple Renderers into LOD levels.</para>
  /// </summary>
  public sealed class LODGroup : Component
  {
    /// <summary>
    ///   <para>The local reference point against which the LOD distance is calculated.</para>
    /// </summary>
    public Vector3 localReferencePoint
    {
      get
      {
        Vector3 vector3;
        this.INTERNAL_get_localReferencePoint(out vector3);
        return vector3;
      }
      set
      {
        this.INTERNAL_set_localReferencePoint(ref value);
      }
    }

    /// <summary>
    ///   <para>The size of the LOD object in local space.</para>
    /// </summary>
    public float size { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   <para>The number of LOD levels.</para>
    /// </summary>
    public int lodCount { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>
    ///   <para>The LOD fade mode used.</para>
    /// </summary>
    public LODFadeMode fadeMode { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   <para>Specify if the cross-fading should be animated by time. The animation duration is specified globally as crossFadeAnimationDuration.</para>
    /// </summary>
    public bool animateCrossFading { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   <para>Enable / Disable the LODGroup - Disabling will turn off all renderers.</para>
    /// </summary>
    public bool enabled { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   <para>The cross-fading animation duration in seconds. ArgumentException will be thrown if it is set to zero or a negative value.</para>
    /// </summary>
    public static extern float crossFadeAnimationDuration { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] set; }

    [WrapperlessIcall]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void INTERNAL_get_localReferencePoint(out Vector3 value);

    [WrapperlessIcall]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void INTERNAL_set_localReferencePoint(ref Vector3 value);

    /// <summary>
    ///   <para>Recalculate the bounding region for the LODGroup (Relatively slow, do not call often).</para>
    /// </summary>
    [WrapperlessIcall]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void RecalculateBounds();

    /// <summary>
    ///   <para>Returns the array of LODs.</para>
    /// </summary>
    /// <returns>
    ///   <para>The LOD array.</para>
    /// </returns>
    [WrapperlessIcall]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public LOD[] GetLODs();

    [Obsolete("Use SetLODs instead.")]
    public void SetLODS(LOD[] lods)
    {
      this.SetLODs(lods);
    }

    /// <summary>
    ///   <para>Set the LODs for the LOD group. This will remove any existing LODs configured on the LODGroup.</para>
    /// </summary>
    /// <param name="lods">The LODs to use for this group.</param>
    [WrapperlessIcall]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void SetLODs(LOD[] lods);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="index">The LOD level to use. Passing index &lt; 0 will return to standard LOD processing.</param>
    [WrapperlessIcall]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void ForceLOD(int index);
  }
}
