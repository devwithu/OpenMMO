//by Fhiz
using OpenMMO;
using OpenMMO.Network;
using OpenMMO.Database;
using OpenMMO.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Mirror;

namespace OpenMMO.Network
{

    /// <summary>
    /// This partial section of NetworkManager adds the GUI scene and loading to it.
    /// </summary>
	public partial class NetworkManager
	{
				
		[Header("GUI Scene")]
		public UnityScene guiScene;

		/// <summary>
    	/// Hooks into AwakePriority to additively load the UI scene.
    	/// </summary>
		[DevExtMethods(nameof(AwakePriority))]
		void AwakePriority_UI()
		{
#if _CLIENT
			// -- load the UI scene only if we are client & UI available
			LoadUIAdditive();
#endif
		}
		
		/// <summary>
    	/// This actually loads the UI scene.
    	/// </summary>
		protected void LoadUIAdditive()
		{
			if (guiScene != null)
				SceneManager.LoadScene(guiScene, LoadSceneMode.Additive);
		}
	
	}

}