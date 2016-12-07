using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using CocosSharp;


namespace CocosSharpFormsTeratail1207
{
	public class GamePage : ContentPage
	{
		CocosSharpView gameView;

		public GamePage()
		{
			var grid = new Grid();
			grid.RowDefinitions = new RowDefinitionCollection {
				new RowDefinition{Height = new GridLength(1, GridUnitType.Star)},
				new RowDefinition{Height = new GridLength(1, GridUnitType.Star)}
			};
			var gameView = new CocosSharpView()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				DesignResolution = new Size(1024, 768),
				BackgroundColor = Color.Aqua,
				ViewCreated = LoadGame
			};
			grid.Children.Add(gameView);

			Content = grid;

			Debug.WriteLine("GamePage init.");
		}

		protected override void OnDisappearing()
		{
			if (gameView != null)
			{
				gameView.Paused = true;
			}

			base.OnDisappearing();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (gameView != null)
				gameView.Paused = false;
		}

		void LoadGame(object sender, EventArgs e)
		{

			Debug.WriteLine("LoadGame");

			var nativeGameView = sender as CCGameView;

			if (nativeGameView != null)
			{
				var contentSearchPaths = new List<string>() { "Fonts", "Sounds" };
				CCSizeI viewSize = nativeGameView.ViewSize;
				CCSizeI designResolution = nativeGameView.DesignResolution;

				// Determine whether to use the high or low def versions of our images
				// Make sure the default texel to content size ratio is set correctly
				// Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
				if (designResolution.Width < viewSize.Width)
				{
					contentSearchPaths.Add("Images/Hd");
					CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
				}
				else
				{
					contentSearchPaths.Add("Images/Ld");
					CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
				}

				nativeGameView.ContentManager.SearchPaths = contentSearchPaths;

				CCScene gameScene = new CCScene(nativeGameView);
				gameScene.AddLayer(new GameLayer());
				nativeGameView.RunWithScene(gameScene);
			}
		}
	}
}
