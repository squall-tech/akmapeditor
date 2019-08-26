using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKMapEditor.OtMapEditor.OtBrush
{
    public class TileAlignement
    {
        public const int TILE_NORTHWEST = 1;
        public const int TILE_NORTH = 2;
        public const int TILE_NORTHEAST = 4;
        public const int TILE_WEST = 8;
        public const int TILE_EAST = 16;
        public const int TILE_SOUTHWEST = 32;
        public const int TILE_SOUTH = 64;
        public const int TILE_SOUTHEAST = 128;

        public const int WALLTILE_NORTH = 1;
	    public const int WALLTILE_WEST = 2;
	    public const int WALLTILE_EAST = 4;
        public const int WALLTILE_SOUTH = 8;
    };

    public class BorderType
    {
        public const int BORDER_NONE = 0;
        public const int NORTH_HORIZONTAL = 1;
        public const int EAST_HORIZONTAL = 2;
        public const int SOUTH_HORIZONTAL = 3;
        public const int WEST_HORIZONTAL = 4;
        public const int NORTHWEST_CORNER = 5;
        public const int NORTHEAST_CORNER = 6;
        public const int SOUTHWEST_CORNER = 7;
        public const int SOUTHEAST_CORNER = 8;
        public const int NORTHWEST_DIAGONAL = 9;
        public const int NORTHEAST_DIAGONAL = 10;
        public const int SOUTHEAST_DIAGONAL = 11;
        public const int SOUTHWEST_DIAGONAL = 12;

        public const int CARPET_CENTER = 13;
	    // Wall types
	    public const int WALL_POLE = 0;
	    public const int WALL_SOUTH_END = 1;
	    public const int WALL_EAST_END = 2;
	    public const int WALL_NORTHWEST_DIAGONAL = 3;
	    public const int WALL_WEST_END = 4;
	    public const int WALL_NORTHEAST_DIAGONAL = 5;
	    public const int WALL_HORIZONTAL = 6;
	    public const int WALL_SOUTH_T = 7;
	    public const int WALL_NORTH_END = 8;
	    public const int WALL_VERTICAL = 9;
	    public const int WALL_SOUTHWEST_DIAGONAL = 10;
	    public const int WALL_EAST_T = 11;
	    public const int WALL_SOUTHEAST_DIAGONAL = 12;
	    public const int WALL_WEST_T = 13;
	    public const int WALL_NORTH_T = 14;
	    public const int WALL_INTERSECTION = 15;
	    public const int WALL_UNTOUCHABLE = 16;
	    // Table types
	    public const int TABLE_NORTH_END = 0;
	    public const int TABLE_SOUTH_END = 1;
	    public const int TABLE_EAST_END = 2;
	    public const int TABLE_WEST_END = 3;
	    public const int TABLE_HORIZONTAL = 4;
	    public const int TABLE_VERTICAL = 5;
        public const int TABLE_ALONE = 6;
    }

    public class Border_Types
    {
        public static int[] border_types;
        public static int[] door_items;
        public static int[] full_border_types;
        public static int[] half_border_types;
        public static int[] carpet_types;
        public static int[] table_types;

        public static void init()
        {
            border_types = new int[256];
            full_border_types = new int[16];
            half_border_types = new int[16];
            table_types = new int[256];
            carpet_types = new int[256];            

            border_types[0] // 0
                = BorderType.BORDER_NONE;
            border_types[TileAlignement.TILE_NORTHWEST] // 1
                = BorderType.NORTHWEST_CORNER;
            border_types[TileAlignement.TILE_NORTH] //10
                = BorderType.NORTH_HORIZONTAL;
            border_types[TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11
                = BorderType.NORTH_HORIZONTAL;
            border_types[TileAlignement.TILE_NORTHEAST] // 100
                = BorderType.NORTHEAST_CORNER;
            border_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101
                = BorderType.NORTHWEST_CORNER | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110
                = BorderType.NORTH_HORIZONTAL;
            border_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111
                = BorderType.NORTH_HORIZONTAL;
            border_types[TileAlignement.TILE_WEST] // 1000
                = BorderType.WEST_HORIZONTAL;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001
                = BorderType.WEST_HORIZONTAL;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1010
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1100
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_EAST] // 10000
                = BorderType.EAST_HORIZONTAL;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 10001
                = BorderType.NORTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 10010
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10011
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 10100
                = BorderType.EAST_HORIZONTAL;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 10101
                = BorderType.NORTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 10110
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10111
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] //11000
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 11001
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 11010
                = BorderType.NORTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11011
                = BorderType.NORTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 11100
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 11101
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 11110
                = BorderType.NORTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11111
                = BorderType.NORTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHWEST] // 100000
                = BorderType.SOUTHWEST_CORNER;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 100001
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 100010
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100011
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 100100
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 100101
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_CORNER << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 100110
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100111
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 101000
                = BorderType.WEST_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 101001
                = BorderType.WEST_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 101010
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101011
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 101100
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101101
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 101110
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101111
                = BorderType.NORTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 110000
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 110001
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 110010
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110011
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 110100
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 110101
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110110
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110111
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 111000
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 111001
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 111010
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111011
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 111100
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 111101
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 111110
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111111
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH] // 1000000
                = BorderType.SOUTH_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHWEST] // 1000001
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH] // 1000010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST] // 1000100
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1000101
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1000110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST] // 1001000
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001001
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1001010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1001100
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1001101
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1001110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST] // 1010000
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1010001
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1010010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1010100
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1010101
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1010110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1011000
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1011001
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1011010
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011011
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1011100
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1011101
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1011110
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011111
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST] // 1100000
                = BorderType.SOUTH_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 1100001
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 1100010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 1100100
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1100101
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8 | BorderType.NORTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1100110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 1101000
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1101001
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1101010
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101011
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1101100
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101101
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1101110
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101111
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 1110000
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1110001
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1110010
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110011
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1110100
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1110101
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110110
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110111
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1111000
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1111001
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1111010
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111011
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1111100
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1111101
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1111110
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111111
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;

            border_types[TileAlignement.TILE_SOUTHEAST]
                = BorderType.SOUTHEAST_CORNER;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHWEST] // 1
                = BorderType.NORTHWEST_CORNER | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTH] //10
                = BorderType.NORTH_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11
                = BorderType.NORTH_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST] // 100
                = BorderType.NORTHEAST_CORNER | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101
                = BorderType.NORTHEAST_CORNER | BorderType.NORTHWEST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110
                = BorderType.NORTH_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111
                = BorderType.NORTH_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST] // 1000
                = BorderType.WEST_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001
                = BorderType.WEST_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1010
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1100
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST] // 10000
                = BorderType.EAST_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 10001
                = BorderType.EAST_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 10010
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10011
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 10100
                = BorderType.EAST_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 10101
                = BorderType.EAST_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 10110
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10111
                = BorderType.NORTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] //11000
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 11001
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 11010
                = BorderType.NORTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11011
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 11100
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 11101
                = BorderType.EAST_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 11110
                = BorderType.NORTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11111
                = BorderType.NORTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST] // 100000
                = BorderType.SOUTHWEST_CORNER | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 100001
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHWEST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 100010
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100011
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 100100
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 100101
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_CORNER << 8 | BorderType.NORTHWEST_CORNER << 16 | BorderType.SOUTHEAST_CORNER << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 100110
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100111
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTH_HORIZONTAL << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 101000
                = BorderType.WEST_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 101001
                = BorderType.WEST_HORIZONTAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 101010
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101011
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 101100
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101101
                = BorderType.WEST_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8 | BorderType.SOUTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 101110
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101111
                = BorderType.NORTHWEST_DIAGONAL | BorderType.SOUTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 110000
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 110001
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 110010
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110011
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 110100
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 110101
                = BorderType.SOUTHWEST_CORNER | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110110
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110111
                = BorderType.SOUTHWEST_CORNER | BorderType.NORTHEAST_DIAGONAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 111000
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 111001
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 111010
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111011
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 111100
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 111101
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 111110
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111111
                = BorderType.WEST_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH] // 1000000
                = BorderType.SOUTH_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHWEST] // 1000001
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH] // 1000010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST] // 1000100
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1000101
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8 | BorderType.NORTHWEST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1000110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST] // 1001000
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001001
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1001010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1001100
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1001101
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1001110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST] // 1010000
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1010001
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1010010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1010100
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1010101
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1010110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1011000
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1011001
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1011010
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011011
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1011100
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1011101
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1011110
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011111
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.EAST_HORIZONTAL << 16 | BorderType.NORTH_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST] // 1100000
                = BorderType.SOUTH_HORIZONTAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 1100001
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 1100010
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100011
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 1100100
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1100101
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTHWEST_CORNER << 8 | BorderType.NORTHEAST_CORNER << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1100110
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100111
                = BorderType.SOUTH_HORIZONTAL | BorderType.NORTH_HORIZONTAL << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 1101000
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1101001
                = BorderType.SOUTHWEST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1101010
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101011
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1101100
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101101
                = BorderType.SOUTHWEST_DIAGONAL | BorderType.NORTHEAST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1101110
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101111
                = BorderType.SOUTH_HORIZONTAL | BorderType.WEST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 1110000
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1110001
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1110010
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110011
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1110100
                = BorderType.SOUTHEAST_DIAGONAL;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1110101
                = BorderType.SOUTHEAST_DIAGONAL | BorderType.NORTHWEST_CORNER << 8;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110110
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110111
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1111000
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1111001
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1111010
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111011
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1111100
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1111101
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.WEST_HORIZONTAL << 16;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1111110
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;
            border_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111111
                = BorderType.SOUTH_HORIZONTAL | BorderType.EAST_HORIZONTAL << 8 | BorderType.NORTH_HORIZONTAL << 16 | BorderType.WEST_HORIZONTAL << 24;


            full_border_types[0] // 0
                = BorderType.WALL_POLE;
            full_border_types[TileAlignement.WALLTILE_NORTH] // 1
                = BorderType.WALL_SOUTH_END;
            full_border_types[TileAlignement.WALLTILE_WEST] // 10
                = BorderType.WALL_EAST_END;
            full_border_types[TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 11
                = BorderType.WALL_NORTHWEST_DIAGONAL;
            full_border_types[TileAlignement.WALLTILE_EAST] // 100
                = BorderType.WALL_WEST_END;
            full_border_types[TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_NORTH] // 101
                = BorderType.WALL_NORTHEAST_DIAGONAL;
            full_border_types[TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST] // 110
                = BorderType.WALL_HORIZONTAL;
            full_border_types[TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 111
                = BorderType.WALL_SOUTH_T;
            full_border_types[TileAlignement.WALLTILE_SOUTH] // 1000
                = BorderType.WALL_NORTH_END;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_NORTH] // 1001
                = BorderType.WALL_VERTICAL;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_WEST] // 1010
                = BorderType.WALL_SOUTHWEST_DIAGONAL;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 1011
                = BorderType.WALL_EAST_T;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST] // 1100
                = BorderType.WALL_SOUTHEAST_DIAGONAL;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_NORTH] // 1101
                = BorderType.WALL_WEST_T;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST] // 1110
                = BorderType.WALL_NORTH_T;
            full_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 1111
                = BorderType.WALL_INTERSECTION;

            half_border_types[0] // 0
                = BorderType.WALL_POLE;
            half_border_types[TileAlignement.WALLTILE_NORTH] // 1
                = BorderType.WALL_VERTICAL;
            half_border_types[TileAlignement.WALLTILE_WEST] // 10
                = BorderType.WALL_HORIZONTAL;
            half_border_types[TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 11
                = BorderType.WALL_NORTHWEST_DIAGONAL;
            half_border_types[TileAlignement.WALLTILE_EAST] // 100
                = BorderType.WALL_POLE;
            half_border_types[TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_NORTH] // 101
                = BorderType.WALL_VERTICAL;
            half_border_types[TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST] // 110
                = BorderType.WALL_HORIZONTAL;
            half_border_types[TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 111
                = BorderType.WALL_NORTHWEST_DIAGONAL;
            half_border_types[TileAlignement.WALLTILE_SOUTH] // 1000
                = BorderType.WALL_POLE;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_NORTH] // 1001
                = BorderType.WALL_VERTICAL;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_WEST] // 1010
                = BorderType.WALL_HORIZONTAL;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 1011
                = BorderType.WALL_NORTHWEST_DIAGONAL;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST] // 1100
                = BorderType.WALL_POLE;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_NORTH] // 1101
                = BorderType.WALL_VERTICAL;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST] // 1110
                = BorderType.WALL_HORIZONTAL;
            half_border_types[TileAlignement.WALLTILE_SOUTH | TileAlignement.WALLTILE_EAST | TileAlignement.WALLTILE_WEST | TileAlignement.WALLTILE_NORTH] // 1111
                = BorderType.WALL_NORTHWEST_DIAGONAL;
            carpet_types[0] // 0
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_NORTHWEST] // 1
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_NORTH] //10
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_NORTHEAST] // 100
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_WEST] // 1000
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1010
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1100
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_EAST] // 10000
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 10001
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 10010
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10011
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 10100
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 10101
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 10110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10111
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] //11000
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 11001
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 11010
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11011
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 11100
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 11101
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 11110
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHWEST] // 100000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 100001
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 100010
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100011
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 100100
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 100101
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 100110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 101000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 101001
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 101010
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101011
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 101100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 101110
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101111
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 110000
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 110001
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 110010
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110011
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 110100
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 110101
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110111
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 111000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 111001
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 111010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111011
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 111100
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 111101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 111110
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH] // 1000000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHWEST] // 1000001
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH] // 1000010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000011
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST] // 1000100
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1000101
                = BorderType.NORTH_HORIZONTAL; ;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1000110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST] // 1001000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001001
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1001010
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001011
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1001100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1001101
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1001110
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST] // 1010000
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1010001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1010010
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010011
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1010100
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1010101
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1010110
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010111
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1011000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1011001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1011010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011011
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1011100
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1011101
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1011110
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST] // 1100000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 1100001
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 1100010
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100011
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 1100100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1100101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1100110
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100111
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 1101000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1101001
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1101010
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101011
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1101100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101101
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1101110
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101111
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 1110000
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1110001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1110010
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110011
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1110100
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1110101
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110110
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110111
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1111000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1111001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1111010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111011
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1111100
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1111101
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1111110
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111111
                = BorderType.NORTHWEST_DIAGONAL;

            carpet_types[TileAlignement.TILE_SOUTHEAST]
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHWEST] // 1
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTH] //10
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST] // 100
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST] // 1000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1010
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1100
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST] // 10000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 10001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 10010
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10011
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 10100
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 10101
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 10110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10111
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] //11000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 11001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 11010
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11011
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 11100
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 11101
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 11110
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST] // 100000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 100001
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 100010
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100011
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 100100
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 100101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 100110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 101000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 101001
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 101010
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101011
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 101100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101101
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 101110
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101111
                = BorderType.NORTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 110000
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 110001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 110010
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110011
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 110100
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 110101
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110110
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110111
                = BorderType.NORTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 111000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 111001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 111010
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111011
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 111100
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 111101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 111110
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111111
                = BorderType.NORTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH] // 1000000
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHWEST] // 1000001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH] // 1000010
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000011
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST] // 1000100
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1000101
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1000110
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000111
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST] // 1001000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001001
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1001010
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001011
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1001100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1001101
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1001110
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001111
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST] // 1010000
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1010001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1010010
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010011
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1010100
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1010101
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1010110
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010111
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1011000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1011001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1011010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011011
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1011100
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1011101
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1011110
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011111
                = BorderType.NORTHEAST_DIAGONAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST] // 1100000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 1100001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 1100010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100011
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 1100100
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1100101
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1100110
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100111
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 1101000
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1101001
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1101010
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101011
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1101100
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101101
                = BorderType.SOUTHWEST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1101110
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101111
                = BorderType.WEST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 1110000
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1110001
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1110010
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110011
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1110100
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1110101
                = BorderType.SOUTHEAST_CORNER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110110
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110111
                = BorderType.EAST_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1111000
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1111001
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1111010
                = BorderType.CARPET_CENTER;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111011
                = BorderType.SOUTHWEST_DIAGONAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1111100
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1111101
                = BorderType.SOUTH_HORIZONTAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1111110
                = BorderType.SOUTHEAST_DIAGONAL;
            carpet_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111111
                = BorderType.CARPET_CENTER;

            table_types[0] // 0
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_NORTHWEST] // 1
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_NORTH] //10
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_NORTHEAST] // 100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_WEST] // 1000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_EAST] // 10000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 10001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 10010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 10100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 10101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 10110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] //11000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 11001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 11010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 11100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 11101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 11110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11111
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST] // 100000
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 100001
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 100010
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100011
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 100100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 100101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 100110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 101000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 101001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 101010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 101100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 101110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 110000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 110001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 110010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 110100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 110101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 111000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 111001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 111010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 111100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 111101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 111110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111111
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH] // 1000000
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHWEST] // 1000001
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH] // 1000010
                = BorderType.TABLE_VERTICAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000011
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST] // 1000100
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1000101
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1000110
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000111
                = BorderType.TABLE_NORTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST] // 1001000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1001010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1001100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1001101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1001110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST] // 1010000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1010001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1010010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1010100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1010101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1010110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1011000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1011001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1011010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1011100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1011101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1011110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011111
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST] // 1100000
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 1100001
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 1100010
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100011
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 1100100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1100101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1100110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 1101000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1101001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1101010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1101100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1101110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 1110000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1110001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1110010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1110100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1110101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1111000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1111001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1111010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1111100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1111101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1111110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111111
                = BorderType.TABLE_HORIZONTAL;

            table_types[TileAlignement.TILE_SOUTHEAST]
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHWEST] // 1
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTH] //10
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST] // 100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST] // 1000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST] // 10000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 10001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 10010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 10100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 10101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 10110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 10111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] //11000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 11001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 11010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 11100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 11101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 11110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 11111
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST] // 100000
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 100001
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 100010
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100011
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 100100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 100101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 100110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 100111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 101000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 101001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 101010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 101100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 101101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 101110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 101111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 110000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 110001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 110010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 110100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 110101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 110110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 110111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 111000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 111001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 111010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 111100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 111101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 111110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 111111
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH] // 1000000
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHWEST] // 1000001
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH] // 1000010
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000011
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST] // 1000100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1000101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1000110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1000111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST] // 1001000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1001001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1001010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1001100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1001101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1001110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1001111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST] // 1010000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1010001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1010010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1010100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1010101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1010110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1010111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1011000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1011001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1011010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1011100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1011101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1011110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1011111
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST] // 1100000
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHWEST] // 1100001
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH] // 1100010
                = BorderType.TABLE_SOUTH_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100011
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST] // 1100100
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1100101
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1100110
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1100111
                = BorderType.TABLE_ALONE;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST] // 1101000
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1101001
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1101010
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101011
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1101100
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1101101
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1101110
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1101111
                = BorderType.TABLE_EAST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST] // 1110000
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHWEST] // 1110001
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH] // 1110010
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110011
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST] // 1110100
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1110101
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1110110
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1110111
                = BorderType.TABLE_WEST_END;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST] // 1111000
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHWEST] // 1111001
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH] // 1111010
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111011
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST] // 1111100
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTHWEST] // 1111101
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH] // 1111110
                = BorderType.TABLE_HORIZONTAL;
            table_types[TileAlignement.TILE_SOUTHEAST | TileAlignement.TILE_SOUTH | TileAlignement.TILE_SOUTHWEST | TileAlignement.TILE_EAST | TileAlignement.TILE_WEST | TileAlignement.TILE_NORTHEAST | TileAlignement.TILE_NORTH | TileAlignement.TILE_NORTHWEST] // 1111111
                = BorderType.TABLE_HORIZONTAL;

        }

    }
}
