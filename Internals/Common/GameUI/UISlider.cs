﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiiPlayTanksRemake.Internals.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WiiPlayTanksRemake.Internals.Common.GameUI
{
    public class UISlider : UIElement
    {
		/// <summary>The value of the <see cref="UISlider"/>. Will only ever range from 0 to 1.</summary>
		public float Value
		{
			get => InternalValue; 
			set => InternalValue = MathHelper.Clamp(value, 0f, 1f);
		}

		/// <summary>The width of the <see cref="UISlider"/>'s bar.</summary>
		public int BarWidth = 5;

		/// <summary>The color of the <see cref="UISlider"/>'s bar.</summary>
		public Color BarColor = Color.Black;

		/// <summary>The color of the <see cref="UISlider"/>.</summary>
		public Color SliderColor = Color.LightBlue;

		private float InternalValue;

		public override void OnInitialize()
		{
			UIImage interactable = new(GameResources.GetGameResource<Texture2D>("Assets/MagicPixel"), 1f, (image, spriteBatch) => spriteBatch.Draw(image.Texture, image.Hitbox, Color.Transparent));
			interactable.Tooltip = Tooltip;
			interactable.SetDimensions((int)Position.X + 2, (int)Position.Y + 2, (int)Size.X - 4, (int)Size.Y - 4);
			interactable.OnLeftDown += (element) =>
			{
				int mouseXRelative = (int)Math.Round(Utilities.GameUtils.MouseX - element.Position.X);
				InternalValue = MathHelper.Clamp(mouseXRelative / element.Size.X, 0, 1);
			};
			Append(interactable);
		}

		public override void DrawSelf(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(GameResources.GetGameResource<Texture2D>("Assets/MagicPixel"), Hitbox, SliderColor);
		}

		public override void DrawChildren(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(GameResources.GetGameResource<Texture2D>("Assets/MagicPixel"), new Rectangle((int)Position.X + (int)(InternalValue * Size.X) - (InternalValue > 0.5 ? BarWidth / 2 : 0), (int)Position.Y - 2, BarWidth, (int)Size.Y + 4), BarColor);
		}
	}
}
