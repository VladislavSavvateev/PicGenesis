using System;

namespace PicGenesis.Model {
	/// <summary>
	/// Цвет.
	/// </summary>
	public struct Color {
		#region Переменные класса
		/// <summary>
		/// Канал красного.
		/// </summary>
		public byte R { get; }
		/// <summary>
		/// Канал зелёного.
		/// </summary>
		public byte G { get; }
		/// <summary>
		/// Канал синего.
		/// </summary>
		public byte B { get; }
		#endregion

		#region Конструкторы
		/// <summary>
		/// Создаёт цвет из RGB24 значений.
		/// </summary>
		/// <param name="R">Канал красного.</param>
		/// <param name="G">Канал зелёного.</param>
		/// <param name="B">Канал синего.</param>
		public Color(byte R, byte G, byte B) {
			this.R = (byte)((int) Math.Floor(R / 16.0) & 0b1110);
			this.G = (byte) ((int) Math.Floor(G / 16.0) & 0b1110);
			this.B = (byte) ((int) Math.Floor(B / 16.0) & 0b1110);
		}
		/// <summary>
		/// Создаёт цвет, копируя все параметры из другого цвета.
		/// </summary>
		/// <param name="color">Другой цвет.</param>
		public Color(Color color) {
			R = color.R;
			G = color.G;
			B = color.B;
		}
		/// <summary>
		/// Создаёт цвет, адаптируя значения каналов из класса System.Drawing.Color.
		/// </summary>
		/// <param name="color">Экземпляр встроенного класса цвета.</param>
		public Color(System.Drawing.Color color) {
			R = (byte) ((int) Math.Floor(color.R / 16.0) & 0b1110);
			G = (byte) ((int) Math.Floor(color.G / 16.0) & 0b1110);
			B = (byte) ((int) Math.Floor(color.B / 16.0) & 0b1110);
		}
		/// <summary>
		/// Создаёт цвет из 16-битного представления в формате VDP.
		/// </summary>
		/// <param name="VDPcolor">16-битное представление в формате VDP.</param>
		public Color(byte[] VDPcolor) {
			B = VDPcolor[0];
			G = (byte) (VDPcolor[1] >> 4);
			R = (byte) (VDPcolor[1] & 0b00001110);
		}
		/// <summary>
		/// Создаёт цвет из соответствующего цвета в палитре VDP.
		/// </summary>
		/// <param name="VDPpallete">Палитра VDP.</param>
		/// <param name="offset">Номер цвета.</param>
		public Color(byte[] VDPpallete, int offset) {
			B = VDPpallete[offset * 2];
			G = (byte) (VDPpallete[offset * 2 + 1] >> 4);
			R = (byte) (VDPpallete[offset * 2 + 1] & 0b00001110);
		}
		#endregion

		#region Методы класса
		/// <summary>
		/// Возвращает 16-битное представление цвета в формате VDP.
		/// </summary>
		/// <returns>16-битное представление цвета.</returns>
		public byte[] ToVDP() {
			byte[] result = new byte[2];
			result[0] = B;
			result[1] = (byte) (G << 4 + R);

			return result;
		}
		/// <summary>
		/// Возвращает упрощённое строковое представление цвета.
		/// </summary>
		/// <returns>Упрощённое строковое представление цвета.</returns>
		public string ToShortString() => $"${R.ToString("X")}{G.ToString("X")}{B.ToString("X")}";
		/// <summary>
		/// Создёт копию текущего цвета.
		/// </summary>
		/// <returns>Копия.</returns>
		public Color CopyTo() => new Color(this);
		#endregion

		#region Переопределённые методы
		/// <summary>
		/// Возвращает строковое представление цвета.
		/// </summary>
		/// <returns>Строковое представление цвета.</returns>
		public override string ToString() => $"Цвет ${R.ToString("X")}{G.ToString("X")}{B.ToString("X")}";
		/// <summary>
		/// Возвращает хэш-код цвета.
		/// </summary>
		/// <returns>Хэш-код цвета.</returns>
		public override int GetHashCode() => R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
		/// <summary>
		/// Сравнивает текущий цвет с другим.
		/// </summary>
		/// <param name="obj">Другой цвет.</param>
		/// <returns>true - если цвета равны.</returns>
		public override bool Equals(object obj) {
			if (!(obj is Color)) return false;
			var c = (Color) obj;
			return R == c.R && G == c.G && B == c.B;
		}
		#endregion
	}
}
