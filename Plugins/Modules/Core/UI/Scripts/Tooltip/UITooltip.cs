//by Fhiz
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OpenMMO;
using OpenMMO.UI;

namespace OpenMMO.UI
{
	
	/// <summary>
    /// Add this component to the UITooltip itself to display it when hovering over a canvas object.
    /// </summary>
	[DisallowMultipleComponent]
	public class UITooltip : UIBase
	{
		
		public Text tooltipText;
		
		public static UITooltip singleton;
		
	 	/// <summary>
    	/// Awake sets the singleton (as there can only be one singleton on screen at the same time) and calls base.Awake
    	/// </summary>
		protected override void Awake()
		{
			singleton = this;
			base.Awake();
		}
		
		/// <summary>
    	/// Shows the tooltip with the given text. The text is usually provided from another part of the code or another object.
   	 	/// </summary>
		public void Show(string _text)
		{
			tooltipText.text = _text;
			base.Show();
		}
		
		/// <summary>
    	/// Updates the tooltip with the given text. The text usually comes from another part of the code.
    	/// </summary>
		public void UpdateTooltip(string _text)
		{
			if (root.activeSelf)
				tooltipText.text = _text;
		}
		
		/// <summary>
    	/// Checks if root object is currently active. Tooltips are only active if their root object is active as well.
    	/// </summary>
		public bool Active
		{
			get {
				return root.activeSelf;
			}
		}
		
	}
	
}