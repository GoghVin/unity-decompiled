using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	/// <summary>
	///   <para>A component for masking children elements.</para>
	/// </summary>
	[AddComponentMenu("UI/Mask", 13), DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
	public class Mask : UIBehaviour, ICanvasRaycastFilter, IMaterialModifier
	{
		[NonSerialized]
		private RectTransform m_RectTransform;

		[FormerlySerializedAs("m_ShowGraphic"), SerializeField]
		private bool m_ShowMaskGraphic = true;

		[NonSerialized]
		private Graphic m_Graphic;

		[NonSerialized]
		private Material m_MaskMaterial;

		[NonSerialized]
		private Material m_UnmaskMaterial;

		/// <summary>
		///   <para>Cached RectTransform.</para>
		/// </summary>
		public RectTransform rectTransform
		{
			get
			{
				RectTransform arg_1C_0;
				if ((arg_1C_0 = this.m_RectTransform) == null)
				{
					arg_1C_0 = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return arg_1C_0;
			}
		}

		/// <summary>
		///   <para>Show the graphic that is associated with the Mask render area.</para>
		/// </summary>
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		/// <summary>
		///   <para>The graphic associated with the Mask.</para>
		/// </summary>
		public Graphic graphic
		{
			get
			{
				Graphic arg_1C_0;
				if ((arg_1C_0 = this.m_Graphic) == null)
				{
					arg_1C_0 = (this.m_Graphic = base.GetComponent<Graphic>());
				}
				return arg_1C_0;
			}
		}

		protected Mask()
		{
		}

		/// <summary>
		///   <para>See:IMask.</para>
		/// </summary>
		[Obsolete("use Mask.enabled instead", true)]
		public virtual bool MaskEnabled()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		///   <para>See:IGraphicEnabledDisabled.</para>
		/// </summary>
		[Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.hasPopInstruction = true;
				this.graphic.SetMaterialDirty();
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		/// <summary>
		///   <para>See MonoBehaviour.OnDisable.</para>
		/// </summary>
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
				this.graphic.canvasRenderer.hasPopInstruction = false;
				this.graphic.canvasRenderer.popMaterialCount = 0;
			}
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = null;
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (!this.IsActive())
			{
				return;
			}
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		/// <summary>
		///   <para>See:ICanvasRaycastFilter.</para>
		/// </summary>
		/// <param name="sp"></param>
		/// <param name="eventCamera"></param>
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		/// <summary>
		///   <para>See: IMaterialModifier.</para>
		/// </summary>
		/// <param name="baseMaterial"></param>
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			if (this.graphic == null)
			{
				return baseMaterial;
			}
			Transform stopAfter = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = MaskUtilities.GetStencilDepth(base.transform, stopAfter);
			if (stencilDepth >= 8)
			{
				Debug.LogError("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int num = 1 << stencilDepth;
			if (num == 1)
			{
				Material maskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, (!this.m_ShowMaskGraphic) ? ((ColorWriteMask)0) : ColorWriteMask.All);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMaterial;
				Material unmaskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, (ColorWriteMask)0);
				StencilMaterial.Remove(this.m_UnmaskMaterial);
				this.m_UnmaskMaterial = unmaskMaterial;
				this.graphic.canvasRenderer.popMaterialCount = 1;
				this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
				return this.m_MaskMaterial;
			}
			Material maskMaterial2 = StencilMaterial.Add(baseMaterial, num | num - 1, StencilOp.Replace, CompareFunction.Equal, (!this.m_ShowMaskGraphic) ? ((ColorWriteMask)0) : ColorWriteMask.All, num - 1, num | num - 1);
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = maskMaterial2;
			this.graphic.canvasRenderer.hasPopInstruction = true;
			Material unmaskMaterial2 = StencilMaterial.Add(baseMaterial, num - 1, StencilOp.Replace, CompareFunction.Equal, (ColorWriteMask)0, num - 1, num | num - 1);
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = unmaskMaterial2;
			this.graphic.canvasRenderer.popMaterialCount = 1;
			this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
			return this.m_MaskMaterial;
		}
	}
}