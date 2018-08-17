using System;

namespace PicGenesis.Model {
	/// <summary>
	/// Палитра цветов.
	/// </summary>
	public class Pallete {
		#region Переменные класса
		/// <summary>
		/// Цвета в палитре.
		/// </summary>
		public Color[] Colors { get; }
		#endregion

		#region Конструкторы
		/// <summary>
		/// Создаёт палитру с чёрными цветами.
		/// </summary>
		public Pallete() { Colors = new Color[16]; }
		/// <summary>
		/// Создаёт палитру, копируя цвета из массива других цветов.
		/// </summary>
		/// <param name="colors">Массив цветов.</param>
		public Pallete(Color[] colors) : this() {
			if (colors == null) throw new ArgumentException();
			if (colors.Length != 16) throw new ArgumentException();

			for (int i = 0; i < 16; i++)
				Colors[i] = colors[i].CopyTo();
		}
		/// <summary>
		/// Создаёт палитру, аналогичную данной палитре VDP.
		/// </summary>
		/// <param name="VDPpallete">Палитра VDP.</param>
		public Pallete(byte[] VDPpallete) : this() {
			for (int i = 0; i < 16; i++) 
				Colors[i] = new Color(VDPpallete, i);
		}
		#endregion

		#region Переопределённые методы
		/// <summary>
		/// Возвращает строковое представление палитры.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var str = "";
			for (int i = 0; i < 16; i++) {
				str += Colors[i].ToShortString();
				if (i != 15) str += " ";
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
		/// <returns>Результат сравнения.</returns>
		public override bool Equals(object obj) {
			if (!(obj is Pallete)) return false;

			var p = (Pallete) obj;
			for (int i = 0; i < 16; i++)
				if (!Colors[i].Equals(p.Colors[i])) return false;

			return true;
		}
		#endregion
	}
}
