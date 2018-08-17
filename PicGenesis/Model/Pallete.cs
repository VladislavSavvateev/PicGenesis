using System;
using System.Collections.Generic;

namespace PicGenesis.Model {
	/// <summary>
	/// Палитра цветов.
	/// </summary>
	public class Pallete {
		#region Переменные класса
		/// <summary>
		/// Цвета в палитре.
		/// </summary>
		public List<Color> Colors { get; }
		#endregion

		#region Конструкторы
		/// <summary>
		/// Создаёт пустую палитру.
		/// </summary>
		public Pallete() { Colors = new List<Color>(); }
		/// <summary>
		/// Создаёт палитру, копируя цвета из листа других цветов.
		/// </summary>
		/// <param name="colors">Лист цветов.</param>
		public Pallete(List<Color> colors) : this() {
			if (colors == null) throw new ArgumentException();
			if (colors.Count > 16) throw new ArgumentException();

			foreach (Color c in colors)
				Colors.Add(c.CopyTo());
		}
		/// <summary>
		/// Создаёт палитру, аналогичную данной палитре VDP.
		/// </summary>
		/// <param name="VDPpallete">Палитра VDP.</param>
		public Pallete(byte[] VDPpallete) : this() {
			for (int i = 0; i < VDPpallete.Length / 2; i++) 
				Colors.Add(new Color(VDPpallete, i));
		}
		#endregion

		#region Переопределённые методы
		/// <summary>
		/// Возвращает строковое представление палитры.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var str = "";
			for (int i = 0; i < Colors.Count; i++) {
				str += Colors[i].ToShortString();
				if (i != Colors.Count - 1) str += " ";
			}
			return str;
		}
		/// <summary>
		/// Возвращает хэш-код палитры.
		/// </summary>
		/// <returns>Хэш-код палитры.</returns>
		public override int GetHashCode() => Colors.GetHashCode();
		/// <summary>
		/// Сравнивает текущую палитру с данной.
		/// </summary>
		/// <param name="obj">Вторая палитра</param>
		/// <returns>true - если палитры равны.</returns>
		public override bool Equals(object obj) {
			if (!(obj is Pallete)) return false;

			var p = (Pallete) obj;
			for (int i = 0; i < Colors.Count; i++)
				if (!Colors[i].Equals(p.Colors[i])) return false;

			return true;
		}
		#endregion
	}
}
