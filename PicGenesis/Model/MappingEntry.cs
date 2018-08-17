using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicGenesis.Model {
	/// <summary>
	/// Элемент маппинга.
	/// </summary>
	public class MappingEntry {
		/// <summary>
		/// Приоритет тайла.
		/// </summary>
		public bool Priority { get; }
		/// <summary>
		/// Индекс привязанной к тайлу палитры.
		/// </summary>
		public byte PalleteIndex { get; }
		/// <summary>
		/// Показатель отражения тайла по вертикали.
		/// </summary>
		public bool VerticalFlip { get; }
		/// <summary>
		/// Показатель отражения тайла по горизонтали.
		/// </summary>
		public bool HorizontalFlip { get; }
		/// <summary>
		/// Порядковый номер тайла.
		/// </summary>
		public ushort TileIndex { get; } 

		/// <summary>
		/// Создаёт элемент маппинга на основе индекса тайла.
		/// </summary>
		/// <param name="tileIndex">Индекс тайла.</param>
		public MappingEntry(ushort tileIndex) => TileIndex = tileIndex;
		/// <summary>
		/// Создаёт элемент маппинга на основе индекса тайла и индекса палитры.
		/// </summary>
		/// <param name="tileIndex">Индекс тайла.</param>
		/// <param name="palleteIndex">Индекс палитры.</param>
		public MappingEntry(ushort tileIndex, byte palleteIndex) : this(tileIndex) 
			=> PalleteIndex = palleteIndex;
		/// <summary>
		/// Создаёт элемент маппинга на основе индекса тайла, индекса палитры и показателей отражений тайла.
		/// </summary>
		/// <param name="tileIndex">Индекс тайла.</param>
		/// <param name="palleteIndex">Индекс палитры.</param>
		/// <param name="verticalFlip">Показатель отражения по вертикали.</param>
		/// <param name="horizontalFlip">Показатель отражения по горизонтали.</param>
		public MappingEntry(ushort tileIndex, byte palleteIndex, bool verticalFlip, bool horizontalFlip) : this(tileIndex, palleteIndex) {
			VerticalFlip = verticalFlip;
			HorizontalFlip = horizontalFlip;
		}
		/// <summary>
		/// Создаёт элемент маппинга на основе индекса тайла, индекса палитры, показателей отражений тайла и показателя приоритета.
		/// </summary>
		/// <param name="tileIndex">Индекс тайла.</param>
		/// <param name="palleteIndex">Индекс палитры.</param>
		/// <param name="verticalFlip">Показатель отражения по вертикали.</param>
		/// <param name="horizontalFlip">Показатель отражения по горизонтали.</param>
		/// <param name="priority">Показатель приоритета.</param>
		public MappingEntry(ushort tileIndex, byte palleteIndex, bool verticalFlip, bool horizontalFlip, bool priority)
			: this(tileIndex, palleteIndex, verticalFlip, horizontalFlip) => Priority = priority;
		/// <summary>
		/// Создаёт элемент маппинга, копируя свойства из другого элемента.
		/// </summary>
		/// <param name="mapEntry">Другой элемент маппинга.</param>
		public MappingEntry(MappingEntry mapEntry) {
			if (mapEntry == null) throw new ArgumentException();

			Priority = mapEntry.Priority;
			PalleteIndex = mapEntry.PalleteIndex;
			VerticalFlip = mapEntry.VerticalFlip;
			HorizontalFlip = mapEntry.HorizontalFlip;
			TileIndex = mapEntry.TileIndex;
		}
		/// <summary>
		/// Создаёт элемент маппинга, извлекая свойства из маппинга VDP.
		/// </summary>
		/// <param name="VDPmap">Маппинг VDP.</param>
		/// <param name="offset">Смещение относительно начала маппинга (в байтах).</param>
		public MappingEntry(byte[] VDPmap, int offset) {
			if (VDPmap == null) throw new ArgumentException();

			Priority = (VDPmap[offset] & 0b10000000) > 0;
			PalleteIndex = (byte) ((VDPmap[offset] & 0b01100000) >> 5);
			VerticalFlip = (VDPmap[offset] & 0b00010000) > 0;
			HorizontalFlip = (VDPmap[offset] & 0b00001000) > 0;
			TileIndex = (byte) ((VDPmap[offset] & 0b00000111) + VDPmap[offset + 1]);
		}

		/// <summary>
		/// Собирает элемент маппинга в пригодный для VDP формат.
		/// </summary>
		/// <returns>Элемент маппинга в VDP формате.</returns>
		public byte[] ToVDP() {
			byte[] result = new byte[2];
			result[0] = (byte) (PalleteIndex << 5 + (TileIndex & 0b11100000000) >> 8 + (Priority ? 0b10000000 : 0) + 
				(VerticalFlip ? 0b00010000 : 0) + (HorizontalFlip ? 0b00001000 : 0));
			result[1] = (byte) (TileIndex & 0b11111111);

			return result;
		}
	}
}
