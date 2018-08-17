using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicGenesis.Model {
	public class Tile {
		public byte[] Pixels { get; }

		public Tile() {
			Pixels = new byte[32];
		}

		public Tile(byte[] pixels) : this() {
			if (pixels == null) throw new ArgumentException();
			if (pixels.Length != 32) throw new ArgumentException();

			for (int i = 0; i < 32; i++) 
				Pixels[i] = pixels[i];
		}

		public override string ToString() => "It's a tile!";
		public override int GetHashCode() => Pixels.GetHashCode();
		public override bool Equals(object obj) {
			if (!(obj is Tile)) return false;

			var t = (Tile) obj;
			for (int i = 0; i < 32; i++)
				if (Pixels[i] != t.Pixels[i]) return false;

			return true;
		}
	}
}
