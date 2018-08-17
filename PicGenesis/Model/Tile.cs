using System;

namespace PicGenesis.Model {
	/// <summary>
	/// Тайл.
	/// </summary>
	public class Tile {
		#region Переменные класса
		/// <summary>
		/// Пиксели тайла.
		/// </summary>
		public byte[] Pixels { get; }
		#endregion

		#region Конструкторы
		/// <summary>
		/// Создаёт пустой тайл, заполненный прозрачным цветом.
		/// </summary>
		public Tile() { Pixels = new byte[32]; }
		/// <summary>
		/// Создаёт тайл на основе тайла VDP.
		/// </summary>
		/// <param name="pixels">Тайл VDP.</param>
		public Tile(byte[] pixels) : this() {
			if (pixels == null) throw new ArgumentException();
			if (pixels.Length != 32) throw new ArgumentException();

			for (int i = 0; i < 32; i++) 
				Pixels[i] = pixels[i];
		}
		#endregion

		#region Переопределённые методы
		/// <summary>
		/// Возвращает строковое представление тайла.
		/// </summary>
		/// <returns>Строковое представление тайла.</returns>
		public override string ToString() => "It's a tile!";
		/// <summary>
		/// Возвращает хэш-код тайла.
		/// </summary>
		/// <returns>Хэш-код тайла.</returns>
		public override int GetHashCode() => Pixels.GetHashCode();
		/// <summary>
		/// Сравнивает текущий тайл с данным.
		/// </summary>
		/// <param name="obj">Второй тайл.</param>
		/// <returns>true - если тайлы равны.</returns>
		public override bool Equals(object obj) {
			if (!(obj is Tile)) return false;

			var t = (Tile) obj;
			for (int i = 0; i < 32; i++)
				if (Pixels[i] != t.Pixels[i]) return false;

			return true;
		}
		#endregion
	}
}
